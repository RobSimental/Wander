using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLocation : MonoBehaviour
{
    private GameObject mainObject;
    private string username = "";
    private string locationX;
    private string locationY;
    private MessageQueue msgQueue;
	private ConnectionManager cManager;
    private float period = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        mainObject = GameObject.Find("MainObject");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_MOVE, ResponseMovement);
        username = DBManager.username;
    }
    void Update()
    {   
        if (period > 3f){
            locationX = "" + transform.position.x;
            locationY = "" + transform.position.y;
            Debug.Log("X: " + locationX + " Y: " + locationY);
            cManager.send(requestMovement(username,locationX,locationY));
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    public void ResponseMovement(ExtendedEventArgs eventArgs){
        ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
    }

    public RequestMovement requestMovement(string username, string locationX, string locationY){
        RequestMovement request = new RequestMovement();
        request.send(username,locationX, locationY);
        return request;
    }
}
