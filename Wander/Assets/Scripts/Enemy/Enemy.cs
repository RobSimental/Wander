using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Enemy : NetworkBehaviour
{
    protected Animator animator;

    public int health, maxHealth;
    private int attack, defense;
    string type;
    public bool isHit, death, attacking;
    protected float attackCoolDownTime, attackCoolDown;
    protected BoxCollider2D hitbox;
    public Player player;


    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();

        type = transform.Find("Tag").tag;
        if (type == "Bat")
        {
            health = maxHealth = 50;
            attack = 20;
            defense = 3;
            attackCoolDown = 2f;
        }
        else if (type == "Goblin")
        {
            health = maxHealth = 50;
            attack = 25;
            defense = 3;
            attackCoolDown = 2f;

        }
        else if (type == "Boss")
        {
            health = maxHealth = 50;
            attack = 20;
            defense = 3;
            attackCoolDown = 3f;

        }/*
        else if (GameObject.FindGameObjectWithTag("Mushroom") != null) {
            health = maxHealth = 75;
            attack = 40;
            defense = 6;
            attackCoolDown = 3f;

        }
        else if (GameObject.FindGameObjectWithTag("Skeleton") != null)
        {
            health = maxHealth = 125;
            attack = 70;
            defense = 6;
            attackCoolDown = 5f;

        }
        */
        hitbox = transform.Find("BasicAttack").GetComponent<BoxCollider2D>();
        isHit = death = attacking = hitbox.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.playerHit(2 + player.defense);
            Debug.Log("Player contacted!");
        }
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        if (death && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            Destroy(gameObject);

        if (isHit && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            isHit = false;
            animator.SetBool("isHit", isHit);
        }

        if (attacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            attacking = hitbox.enabled = false;
            animator.SetBool("attacking", attacking);
        }
    }

    public void Attack()
    {
        if (!death && attackCoolDownTime <= Time.time)
        {
            attackCoolDownTime = Time.time + attackCoolDown;
            attacking = true;
            animator.SetBool("attacking", attacking);
        }
    }

    public void EnableEnemyHitbox()
    {
        hitbox.enabled = true;
    }
    public void DisableEnemyHitbox()
    {
        hitbox.enabled = false;
    }

    public void Hit(int damage)
    {
        Debug.Log("health: " + health);
        health -= damage - defense;
        isHit = true;
        animator.SetBool("isHit", isHit);
        animator.SetTrigger("hit");

        if (health <= 0)
        {
            Destroy(GetComponent<Rigidbody2D>());
            death = true;
            animator.SetBool("dying", death);
            //player.addScore();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.playerHit(attack);
            Debug.Log("Player hit!");
        }
    }

    public int getHealth(){
        return health;
    }
}
