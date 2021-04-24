﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NetworkManagerWander : NetworkManager {

    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerWander roomPlayerPrefab = null;
    [Header("Player Prefabs")]
    [SerializeField] private GameObject Archer = null;
    [SerializeField] private GameObject Mage = null;
    [SerializeField] private GameObject Warrior = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    //list of players in lobby
    public List<NetworkRoomPlayerWander> RoomPlayers { get; } = new List<NetworkRoomPlayerWander>();
    public List<GameObject> GamePlayers { get; } = new List<GameObject>();

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Client Connected to the server");
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
        if(SceneManager.GetActiveScene().path != menuScene)
        {
            //remove to allow players to join active game
            conn.Disconnect();
            return;
        }
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            Debug.Log("WE ARE NOW GOD");
            bool isLeader = RoomPlayers.Count == 0;
            NetworkRoomPlayerWander roomPlayerInstance = Instantiate(roomPlayerPrefab);
            roomPlayerInstance.IsLeader = isLeader; //lets you tell a client if you are the leader
            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
        else
        {
            Debug.Log("MENUS STRIKES BACK");
        }

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerWander>();
            RoomPlayers.Remove(player);
            NotifyPlayersOfReadyState();
        }
        base.OnServerDisconnect(conn);
    }
    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach(var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        foreach(var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }
        return true;
    }
    public void StartGame()
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }
            ServerChangeScene("Wander(LevelProto)");
        }
    }
    public override void ServerChangeScene(string newSceneName)
    {
        //going from menu to game scene
        if (SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("Wander"))
        {
            Debug.Log(RoomPlayers.Count);
            for (int i = RoomPlayers.Count-1; i>=0; i--)
            {
                GameObject gamePlayerPrefab;
                if (RoomPlayers[i].characterChoice == "Mage")
                {
                    gamePlayerPrefab = Mage;
                }
                else if (RoomPlayers[i].characterChoice == "Warrior")
                {
                    gamePlayerPrefab = Warrior;
                }
                else
                {
                    gamePlayerPrefab = Archer;
                }
                Debug.Log("hello:"+ RoomPlayers[i].characterChoice);
                var conn = RoomPlayers[i].connectionToClient;
                var gameplayerInstance = Instantiate(gamePlayerPrefab);
                //gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
            }
        }
        base.ServerChangeScene(newSceneName);
    }
















    /*    public class ClassMessage : MessageBase {
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
        }*/
}