using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class EnemySpawner : NetworkBehaviour
{
    public GameObject batPrefab;
    public GameObject goblinPrefab;
    public GameObject bossPrefab;

    // public int numOfEnemies;

    public override void OnStartServer(){
        Vector2 spawnPosition = new Vector2(6.18f, -6.421f);
        Quaternion spawnRotation = Quaternion.Euler(0.0f, 0, 0);
        
        GameObject bat1 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat1);

        spawnPosition = new Vector2(5.23f, -4.943f);
        GameObject bat2 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat2);

        spawnPosition = new Vector2(6.325f, -3.93f);
        GameObject bat3 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat3);

        spawnPosition = new Vector2(12.071f, -6.474f);
        GameObject bat4 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat4);

        spawnPosition = new Vector2(11.368f, -6.374f);
        GameObject bat5 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat5);

        spawnPosition = new Vector2(10.691f, -3.705f);
        GameObject bat6 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat6);

        spawnPosition = new Vector2(-1.059f, 3.222f);
        GameObject bat7 = (GameObject)Instantiate(batPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bat7);

        //------------------------------------------------------------------------------

        spawnPosition = new Vector2(5.64f, -1.261f);
        GameObject goblin1 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin1);

        spawnPosition = new Vector2(-2.368f, -1.7f);
        GameObject goblin2 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin2);

        spawnPosition = new Vector2(-1.815f, 0.38f);
        GameObject goblin3 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin3);

        spawnPosition = new Vector2(-5.283f, -1.256f);
        GameObject goblin4 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin4);

        spawnPosition = new Vector2(-10.439f, -2.816f);
        GameObject goblin5 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin5);

        spawnPosition = new Vector2(-15.165f, 2.018f);
        GameObject goblin6 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin6);

        spawnPosition = new Vector2(-3.62f, 6.11f);
        GameObject goblin7 = (GameObject)Instantiate(goblinPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(goblin7);

        //-------------------------------------------------------------------------------------

        spawnPosition = new Vector2(9.129f, 3.1f);
        GameObject boss = (GameObject)Instantiate(bossPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(boss);
    }
}
