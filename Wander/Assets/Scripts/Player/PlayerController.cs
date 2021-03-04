using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Mirror;
[System.Serializable]

public class PlayerController : NetworkBehaviour
{
    
    public bool isTired = false;

   

    public void setTired(bool val)
    {
        isTired = val;
    }
   

}