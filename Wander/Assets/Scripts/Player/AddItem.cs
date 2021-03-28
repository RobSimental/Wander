using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PlayFab;
using PlayFab.ServerModels;
public class AddItem : NetworkBehaviour
{
    public LayerMask ground;
    private Transform groundCheck;
    private Rigidbody2D rb;
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 10f);
    }
   
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();           
            player.CmdAddItemToUser(this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
  

}
