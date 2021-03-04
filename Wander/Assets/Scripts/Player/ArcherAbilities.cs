using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Mirror;
public class ArcherAbilities : NetworkBehaviour
{
    Animator animator;
    Player player;

    public GameObject Arrowshower,ArrowshowerLeft, FirstArrow,FirstArrowLeft;
    Vector2 showerPOS, arrowPos;
    public float fireRate1 = 10.0f;
    float nextFire1 = 0f;
    public bool ability1;
    bool active1 = false;

    public GameObject RightFire, LeftFire;
    Vector2 projectilePOS;
    public float fireRate2 = 0.5f;
    float nextFire2 = 0.0f;

    Rigidbody2D rb2D;
    public GameObject A1, A2, A3, A4, A5;
    Vector2 projectilePOS3;
    Vector3 velocity = Vector3.zero;
    Vector2 showerPOS3;
    public float fireRate3 = 0.5f;
    float nextFire3 = 0.0f;

    //ability1Hitbox = GameObject.Find("Ability1").GetComponent<BoxCollider2D>();
    bool right;
    
    //playerScript.facingRight
    // Start is called before the first frame update
    void Awake()
    {
        
        animator = GetComponentInParent<Animator>();
        player = GetComponent<Player>();
        ability1 = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer){
            return;
        }
        if (!player.dying)
        {
            if (Input.GetButtonDown("Ability1") && !ability1 && Time.time > nextFire1)
            {

                right = GetComponentInParent<MageController>().facingRight;
                ability1 = true;
                animator.SetTrigger("ability1");
                nextFire1 = Time.time + fireRate1;
                showerPOS = transform.position;
                if (right)
                    showerPOS += new Vector2(+0.4f, 0.5f);
                else
                    showerPOS += new Vector2(-0.4f, 0.5f);
                CmdFire(showerPOS);



                // Needs to ba a function to call on the server... using Cmd

                // NetworkServer.Spawn(obj);
            }

            if (Input.GetButtonDown("Ability2") && Time.time > nextFire2)
            {
                nextFire2 = Time.time + fireRate2;
                animator.SetTrigger("ability2");
                right = GetComponentInParent<MageController>().facingRight;
                if (right)
                {
                    CmdFirePierceRight();
                }
                else
                {
                    CmdFirePierceLeft();
                }
            }

            if (Input.GetButtonDown("Ability3") && Time.time > nextFire3)
            {
                nextFire3 = Time.time + fireRate3;
                animator.SetTrigger("ability3");

                CmdFire3();
            }
        }






    }

    [Command]
    void CmdFire(Vector2 adjustPos)
    {
        arrowPos = transform.position;
        if (right)
        {
            GameObject obj = Instantiate(FirstArrow, arrowPos, Quaternion.identity);
            NetworkServer.Spawn(obj);
            GameObject arrowShower = Instantiate(Arrowshower, adjustPos, Quaternion.identity);
            NetworkServer.Spawn(arrowShower);
        }
        else {
            GameObject objL = Instantiate(FirstArrowLeft, arrowPos, Quaternion.identity);
            NetworkServer.Spawn(objL);
            GameObject arrowShower = Instantiate(ArrowshowerLeft, adjustPos, Quaternion.identity);
            NetworkServer.Spawn(arrowShower);
        }
       


    }
    [Command]
    void CmdFirePierceRight()
    {
        projectilePOS = transform.position;
        projectilePOS += new Vector2(+.1f, 0.05f);
        GameObject obj = Instantiate(RightFire, projectilePOS, Quaternion.identity);
        NetworkServer.Spawn(obj);
        Destroy(obj, 5f);
    }
    
    [Command]
    void CmdFirePierceLeft(){
        projectilePOS = transform.position;
        projectilePOS += new Vector2(-.1f, 0.05f);
        GameObject obj = Instantiate(LeftFire, projectilePOS, Quaternion.identity);
        NetworkServer.Spawn(obj);
        Destroy(obj, 5f);
    }
    [Command]
    void CmdFire3()
    {
        showerPOS3 = transform.position;
        showerPOS3 += new Vector2(0.3f, 0f);
        GameObject obj1 = Instantiate(A1, showerPOS3, Quaternion.identity);
        NetworkServer.Spawn(obj1);

        showerPOS3 = transform.position;
        showerPOS3 += new Vector2(0.15f, 0.15f);
        GameObject obj2 =Instantiate(A2, showerPOS3, Quaternion.identity);
        NetworkServer.Spawn(obj2);

        showerPOS3 = transform.position;
        showerPOS3 += new Vector2(0f, 0.3f);
        GameObject obj3 =Instantiate(A3, showerPOS3, Quaternion.identity);
        NetworkServer.Spawn(obj3);

        showerPOS3 = transform.position;
        showerPOS3 += new Vector2(-0.15f, 0.15f);
        GameObject obj4 =Instantiate(A4, showerPOS3, Quaternion.identity);
        NetworkServer.Spawn(obj4);

        showerPOS3 = transform.position;
        showerPOS3 += new Vector2(-0.3f, 0f);
        GameObject obj5 =Instantiate(A5, showerPOS3, Quaternion.identity);
        NetworkServer.Spawn(obj5);
        /*
        float xMove = 5;
        Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, rb2D.velocity.x);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
        */
        //implement rolling forward
    }
    void FixedUpdate()
    {
        if(!isLocalPlayer){
            return;
        }
        // Wait for attacking animation to finish
        if (ability1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            ability1 = false;
    }
}
