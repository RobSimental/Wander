using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetName : MonoBehaviour
{
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        if(DBManager.LoggedIn) {
            playerName.text = DBManager.username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
