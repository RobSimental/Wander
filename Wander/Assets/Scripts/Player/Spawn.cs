using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Spawn : MonoBehaviour
{
    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;    

    public GameObject playerPrefab;

    public static int connectionId;
    void Awake()
    {
        mainObject = GameObject.Find("MainObject");        
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_SPAWN, ResponseSpawn);
    }

    private void Start()
    {
        if (cManager)
        {
            cManager.setupSocket();
        }
        cManager.send(requestSpawn(DBManager.id));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePlayer(int id, bool isMyPlayer){
        GameObject go = Instantiate(playerPrefab);
        go.name = "Player: " + id;
        if(isMyPlayer){
            go.AddComponent<PlayerController>();
            go.AddComponent<Camera>();
        }
        GameManager.instance.playerList.Add(id, go);
    }
    
    public RequestSpawn requestSpawn(int id) {
		RequestSpawn request = new RequestSpawn();
		request.send(id);
		return request;
	}
	
	public void ResponseSpawn(ExtendedEventArgs eventArgs) {
		ResponseSpawnEventArgs args = eventArgs as ResponseSpawnEventArgs;
		if (args.status == 0) {
            if(args.id == DBManager.id){
                InstantiatePlayer(args.id,true);
            } else {
                InstantiatePlayer(args.id,false);
            }
			Debug.Log("User Spawned");
		} else {
			Debug.Log("User Not Spawned");
		}
	}
}
