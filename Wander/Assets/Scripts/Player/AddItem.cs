using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PlayFab;
using PlayFab.ServerModels;
public class AddItem : NetworkBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private LayerMask itemLayer, enemyLayer, projectileLayer;
    void Start()
    {
        //moving player out of default layer causes bugs so beware
        itemLayer = LayerMask.NameToLayer("Item");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        projectileLayer = LayerMask.NameToLayer("Projectiles");

        //item jump on spawn
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, 2f);
        
        //ignore collision between items,projectiles,enemies
        Physics2D.IgnoreLayerCollision(itemLayer.value,itemLayer.value);
        Physics2D.IgnoreLayerCollision(itemLayer.value,projectileLayer.value);
        Physics2D.IgnoreLayerCollision(itemLayer.value,enemyLayer.value);
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
