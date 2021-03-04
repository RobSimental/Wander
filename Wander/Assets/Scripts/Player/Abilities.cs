using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Abilities : NetworkBehaviour
{
    Animator animator;

    Player player;
    BoxCollider2D ability1Hitbox, ability3Hitbox;

    public bool actionActive;
    public bool ability1, ability2, ability2BuffActive, ability3, ability3Held;
    private float ability2BuffTimer;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();
        ability1Hitbox = GameObject.Find("Ability1").GetComponent<BoxCollider2D>();
        ability3Hitbox = GameObject.Find("Ability3").GetComponent<BoxCollider2D>();
        ability1Hitbox.enabled = ability3Hitbox.enabled = false;
        actionActive = ability1 = ability2 = ability2BuffActive = ability3 = ability3Held = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isLocalPlayer)
        {   
            return;
        }
        if (ability2BuffActive && Time.time >= ability2BuffTimer)
        {
            player.defense -= 5;
            ability2BuffActive = false;
        }
        if (!player.dying && !actionActive)
        {
            actionActive = true;
            if (Input.GetButtonDown("Ability1"))
            {
                ability1 = true;
                animator.SetTrigger("ability1");
            }
            else if (Input.GetButtonDown("Ability2") && Time.time >= ability2BuffTimer)
            {
                ability2BuffTimer = Time.time + 10; // Defense buff lasts for 10 seconds
                ability2 = ability2BuffActive = true;
                animator.SetTrigger("ability2");
                player.defense += 5;
            }
            else if (Input.GetButtonDown("Ability3"))
            {
                ability3 = ability3Held = true;
                animator.SetBool("actionActive", actionActive);
                animator.SetTrigger("ability3");
            }
            else
                actionActive = false;
            animator.SetBool("actionActive", actionActive);
        }
        else
        {
            if (ability3 && !Input.GetButton("Ability3"))
            {
                actionActive = ability3 = ability3Held = false;
                animator.SetBool("actionActive", actionActive);
                animator.SetBool("ability3Held", false);
            }
        }
    }

    void FixedUpdate()
    {   
        if (!isLocalPlayer)
        {   
            return;
        }
        if (actionActive)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                if (ability3Held)
                    animator.SetBool("ability3Held", true);
                else
                {
                    actionActive = ability1 = ability2 = ability3 = false;
                    animator.SetBool("actionActive", actionActive);
                }
            }
        }
        else
            DisableAbility3Hitbox(); // In case the ability being used was ability 3
    }

    public void EnableAbility1Hitbox()
    {
        ability1Hitbox.enabled = true;
    }

    public void DisableAbility1Hitbox()
    {
        ability1Hitbox.enabled = false;
    }

    public void EnableAbility3Hitbox()
    {
        ability3Hitbox.enabled = true;
    }

    public void DisableAbility3Hitbox()
    {
        ability3Hitbox.enabled = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (ability1Hitbox.enabled && ability1Hitbox.IsTouching(collision) && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.isHit)
            {
                enemy.Hit(player.attack * 2);
                Debug.Log("Enemy hit by ability 1!");
            }
        }
    }
}