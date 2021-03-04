using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackScript : MonoBehaviour
{
    public float velX ;
    public float velY;
    public int damage;
    CircleCollider2D hitbox;
    Animator animator;
    Rigidbody2D rb;// Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator = GetComponentInParent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            velX = 0f;
            animator.SetTrigger("hits");
            Destroy(gameObject);
            enemy.Hit(damage); 
            //Debug.Log("Enemy Hit");

        }
        else if (collision.gameObject.CompareTag("Player")) {
            velX = 0f;
            animator.SetTrigger("hits");
            Player player= collision.gameObject.GetComponent<Player>();
            player.playerHit(damage);
            Destroy(gameObject);
        }//add enviroment wall interaction
        else if (collision.gameObject && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
