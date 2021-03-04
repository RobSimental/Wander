using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour
{
    public int damage;
    public float velX = 0.0f;
    Animator animator;
    CircleCollider2D hitbox;
    public float velY = -0.5f;
    Rigidbody2D rb;// Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX,velY);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("hits");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Hit(damage);
            Debug.Log("Its a hit!");
            Destroy(gameObject,1f);
        }
    }

}

