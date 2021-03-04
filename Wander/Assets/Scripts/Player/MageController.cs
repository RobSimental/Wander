using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Mirror;
[System.Serializable]
public class MageController : PlayerController{  
    public Camera myCam;

    public AudioListener myAud;
    Rigidbody2D rb2D;
    private Animator animator;
    Player player;
   // BasicAttack basicAttack;


    public LayerMask ground;
    private Transform groundCheck;

    public float moveSpeed = 16f;
    public float sprintSpeed = 24f;
    float xMove = 0f;

    public float jumpVelocity = 7f;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;
    Vector3 velocity = Vector3.zero;

    public bool isGrounded;
    public bool facingRight = true;

    public GameObject RightFire, LeftFire;
    Vector2 projectilePOS;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    public bool attacking;
    void Awake()
    {
        attacking = false;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        //basicAttack = GetComponentInChildren<BasicAttack>();
       
        groundCheck = transform.GetChild(0);
        myCam.enabled = false;
        myAud.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        else
        {
            if (myCam.enabled == false)
            {
                myCam.enabled = true;
            }
            if (myAud.enabled == false)
            {
                myAud.enabled = true;
            }
        }//(!player.dying && ((!basicAttack.attacking) || !isGrounded))
        if (!player.dying)
        {
            // Set horizontal movement
            if (Input.GetButton("Shift") && !isTired)
                xMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
            else
                xMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

            animator.SetFloat("Speed", Mathf.Abs(xMove));

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb2D.velocity = Vector2.up * jumpVelocity;
                animator.SetBool("isJumping", true);
            }
        }
        else
            xMove = 0;

        // Flip the players
        if ((xMove > 0 && !facingRight) || (xMove < 0 && facingRight)){
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (Input.GetButtonDown("Attack") && !player.dying && Time.time > nextFire)
        {
            Debug.Log("Im firing my balls");
            animator.SetTrigger("attack");
            nextFire = Time.time + fireRate;
            if(facingRight){
                CmdFireArrow();
            } else {
                CmdFireArrowLeft();
            }
        }
    }

    void FixedUpdate()
    {
        //if (attacking && (AnimFireRight.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            //attacking = false;
        if (!isLocalPlayer)
        {
            return;
        }
        else
        {
            if (myCam.enabled == false)
            {
                myCam.enabled = true;
            }
            if (myAud.enabled == false)
            {
                myAud.enabled = true;
            }
        }
        // Physics to control jump height depending on how long the Jump button is pressed
        if (rb2D.velocity.y < 0)
            rb2D.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
            rb2D.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        animator.SetFloat("yVelocity", rb2D.velocity.y);

        // Checking if player is grounded or not
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .1f, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            // Ignore one-way platforms if currently jumping up (not falling)
            if (colliders[i].CompareTag("One-way"))
                // Using > 0 instead of > 0.2 causes some issues with jumping even when standing still on platform
                if (rb2D.velocity.y > 0.2)
                    continue;
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    animator.SetBool("isJumping", false);
                    animator.SetFloat("yVelocity", 0);
                }
            }
        }

        Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
    }

    // Flips the sprite
    
    // void Flip()
    // {
    //     facingRight = !facingRight;
    //     Vector3 scale = transform.localScale;
    //     scale.x *= -1;
    //     transform.localScale = scale;
    // }
    /*
    void EnableHitbox()
    {
        basicAttack.EnableHitbox();
    }

    void DisableHitbox()
    {
        basicAttack.DisableHitbox();
    }
    */
    // Used for moving forward on warrior bassic attack
    void MoveOnAttack()
    {
        float xMove;
        if (facingRight)
            xMove = 50;
        else
            xMove = -50;

        if (isGrounded)
        {
            Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, 0);
            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
        }
    }
    public void setBackground()
    {
        GameObject.Find("map00").GetComponent<ScrollBackground>().target = rb2D;
        GameObject.Find("map01").GetComponent<ScrollBackground>().target = rb2D;
        GameObject.Find("map02").GetComponent<ScrollBackground>().target = rb2D;
        GameObject.Find("map03").GetComponent<ScrollBackground>().target = rb2D;
    }

    public override void OnStartLocalPlayer()
    {
        setBackground();
    }
    [Command]
    void CmdFireArrow()
    {
        projectilePOS = transform.position;
        projectilePOS += new Vector2(+.1f, 0.05f);
        GameObject obj = Instantiate(RightFire, projectilePOS, Quaternion.identity);
        NetworkServer.Spawn(obj);
        Destroy(obj,5f);
    }
    [Command]
    void CmdFireArrowLeft(){
        projectilePOS = transform.position;
        projectilePOS += new Vector2(-.1f, 0.05f);
        GameObject obj = Instantiate(LeftFire, projectilePOS, Quaternion.identity);
        NetworkServer.Spawn(obj);
        Destroy(obj,5f);
    }
}