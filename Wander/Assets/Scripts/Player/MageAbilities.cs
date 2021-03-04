using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Mirror;

//Ability 3 is bugged where it won't activate after a while
public class MageAbilities : NetworkBehaviour
{
    public Animator animator;

    public GameObject fireshower,Left;
    Vector2 showerPOS;
    public float fireRate1 = 0.5f;
    float nextFire1 = 0.0f;
    public bool ability1;


  

    public bool ability2;
    public Animator animatorMin;
    public GameObject portal, minion;
    Vector2 projectilePOS;
    Vector2 minionPOS;
    public float fireRate2 = 0.5f;
    float nextFire2 = 0.0f;

    public GameObject Fire;
    public float fireRate3 = 0.5f;
    float nextFire3 = 0.0f;
    public bool ability3;
    bool right;
    //public float fireRate = 0.5f;
   // float nextFire = 0.0f;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        ability1 = ability2 = ability3 = false;

        animatorMin = GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) {
            return;
        }
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
            CmdFire1(showerPOS);

        } else if (Input.GetButtonDown("Ability2") && Time.time > nextFire2) {
            nextFire2 = Time.time + fireRate2;
            minionPOS = transform.position;
            minionPOS += new Vector2(+0.3f, -0.043f);
            animator.SetTrigger("ability2");

            CmdSummon(minionPOS);
        }
        else if (Input.GetButtonDown("Ability3") && Time.time > nextFire3) {
            ability3 = true;
            animator.SetTrigger("ability3");
            nextFire3 = Time.time + fireRate3;
            CmdFireWall();
        }
        



    }


    void FixedUpdate()
    {
        if (!isLocalPlayer) {
            return;
        }
        if (ability1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            ability1 = false;
        // Wait for attacking animation to finish
        if (ability2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            ability2 = false;
        if (ability3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            ability3 = false;
    }
    [Command]
    void CmdFire1(Vector2 adjustPos)
    {
        if (right)
        {
            GameObject obj = Instantiate(fireshower, adjustPos, Quaternion.identity);
            NetworkServer.Spawn(obj);
        }
        else {
            GameObject obj = Instantiate(Left, adjustPos, Quaternion.identity);
            NetworkServer.Spawn(obj);
        }
       
    }

    [Command]
    void CmdSummon(Vector2 minPOS)

    {
        GameObject Port = Instantiate(portal, minPOS, Quaternion.identity);
        NetworkServer.Spawn(Port);
        GameObject min = Instantiate(minion, minPOS, Quaternion.identity);
        NetworkServer.Spawn(min);
    }

    [Command]
    void CmdFireWall()
    {
        GameObject Wall= Instantiate(Fire, transform.position, Quaternion.identity);
        NetworkServer.Spawn(Wall);
    }



}