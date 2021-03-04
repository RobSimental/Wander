using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class ClampName : NetworkBehaviour
{
    public Text nameLabel;
    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer){
            return;
        }
        nameLabel = GameObject.Find("PlayerName").GetComponent<SetName>().playerName;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!isLocalPlayer){
            return;
        }
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;
    }
}
