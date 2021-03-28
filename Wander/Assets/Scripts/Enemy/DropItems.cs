using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DropItems : NetworkBehaviour
{
    public GameObject primaryDropPrefab;
    public GameObject secondaryDropPrefab; 
    public void Drops(Transform transform)
    {
        if(primaryDropPrefab != null)
        {
            DropItem(primaryDropPrefab, transform);
        }
        else
        {
            Debug.Log("no primary drop");

        }

        if (secondaryDropPrefab != null)
        {
            DropItem(secondaryDropPrefab, transform);
        }
        else
        {
            Debug.Log("no secondary drop");
        }
    }
    
    private void DropItem(GameObject dropPrefab,Transform transform)
    {
        Vector2 spawnPosition = new Vector2(transform.position.x,transform.position.y);
        GameObject drop = (GameObject)Instantiate(dropPrefab, spawnPosition, Quaternion.Euler(0.0f, 0, 0));
        //this is necassary beacuse we use item name to add the item to inventory
        //normally drop name is: drop(clone)
        drop.name = dropPrefab.name;
        Debug.Log(drop.name);
        NetworkServer.Spawn(drop);
    }
}
