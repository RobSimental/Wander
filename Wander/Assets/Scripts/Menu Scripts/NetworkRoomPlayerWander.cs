using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;
public class NetworkRoomPlayerWander : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[3];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[3];
    [SerializeField] private Button startGameButton = null;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;

    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(true);
        }
    }
    private NetworkManagerWander room;
    private NetworkManagerWander Room //allows you to reference NMWander as Room
    {
        get
        {
            if(room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerWander;
        }
    }
    
    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayFabLogin.username);
        lobbyUI.SetActive(true); //TO ONLY SHOW THE UI FOR YOUR CLIENT
    }
    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        UpdateDisplay();
    }
    public override void OnNetworkDestroy()
    {
        Room.RoomPlayers.Remove(this);
        UpdateDisplay();
    }

    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();
    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();

    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach(var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }
            return;
        }

        for(int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Wating for Player...";
            playerReadyTexts[i].text = string.Empty;
        }
        for(int i = 0; i<Room.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
                "<color=green>Ready</color>" :
                "<color=red>Not Ready</color>";

        }
    }
    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }
        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }
    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;
        Room.NotifyPlayersOfReadyState();
    }
    [Command]
    public void CmdStartGame()
    {
        //only leader can start the game
        if(Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }
    }
}
