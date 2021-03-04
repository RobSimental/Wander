using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkManagerWander : NetworkManager {

    public class ClassMessage : MessageBase {
        public string chosenClass;
    }

    public override void OnStartServer(){
        base.OnStartServer();
        NetworkServer.RegisterHandler<ClassMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect(NetworkConnection conn){
        base.OnClientConnect(conn);
        ClassMessage mess = new ClassMessage{
            chosenClass = DBManager.choosen
        };
        // conn.Send(mess);
        // ClientScene.AddPlayer(conn);
        conn.Send(mess);
    }

    void OnCreateCharacter(NetworkConnection conn, ClassMessage message){
        if(message.chosenClass == "Mage"){
            GameObject player = Instantiate(Resources.Load("characters/Mage", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player);
        } else if (message.chosenClass == "Warrior"){
            GameObject player = Instantiate(Resources.Load("characters/Warrior", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player);
        } else {
            GameObject player = Instantiate(Resources.Load("characters/Ranger", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn){
        // On ServerAddPlayer do nothing
    }
}