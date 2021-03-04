using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BasicAttack : NetworkBehaviour
{
    Animator animator;

    Player player;
    BoxCollider2D hitbox;

    public bool attacking;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        player = GetComponent<Player>();
        hitbox = GameObject.Find("BasicAttack").GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {   
            return;
        }
        if (!hitbox)
            hitbox = GameObject.Find("BasicAttack").GetComponent<BoxCollider2D>();
        if (!attacking && hitbox.enabled)
            hitbox.enabled = false;
        if (Input.GetButtonDown("Attack") && !player.dying && !attacking)
        {
            attacking = true;
            animator.SetTrigger("attack");
        }
    }

    void FixedUpdate()
    {   
        if (!isLocalPlayer)
        {   
            return;
        }
        // Wait for attacking animation to finish
        if (attacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            attacking = false;
    }
    
    // Hitbox enabled on specific frame of animation
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (hitbox.enabled && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.isHit)
            {
                enemy.Hit(player.attack);
                Debug.Log("Enemy hit by basic attack!");
            }
        }
    }
}
