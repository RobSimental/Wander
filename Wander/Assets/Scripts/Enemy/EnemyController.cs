using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class EnemyController : NetworkBehaviour
{
    Rigidbody2D rb2D;
    private Animator animator;
    Enemy enemy;
    string type;

    public LayerMask ground;

    public float moveSpeed;
    public float xMove = 0f;

    public float jumpVelocity;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    Vector3 velocity = Vector3.zero;

    public bool facingRight = true;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        type = transform.Find("Tag").tag;
        if (type == "Bat")
        {
            moveSpeed = 1f;
            jumpVelocity = fallMultiplier = lowJumpMultiplier = 0f;
        }
        else if (type == "Goblin")
        {
            moveSpeed = 2f;
            jumpVelocity = fallMultiplier = lowJumpMultiplier = 2f;
        }
        else if (type == "Boss")
        {
            moveSpeed = 1.5f;
            jumpVelocity = fallMultiplier = lowJumpMultiplier = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.death)
        {
            if ((xMove > 0 && !facingRight) || (xMove < 0 && facingRight))
                Flip();

            if (enemy.isHit)
                KnockBack();
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(xMove));
    }

    // Flips the sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void KnockBack()
    {
        float xMove;
        if (facingRight)
            xMove = -5;
        else
            xMove = 5;

        Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
    }
}
