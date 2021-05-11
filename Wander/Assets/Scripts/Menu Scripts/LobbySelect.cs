using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
public class LobbySelect : NetworkBehaviour
{
    private NetworkManagerWander networkManager;
    [SerializeField] private InputField ipAddressInputField = null;
    public GameObject JoinPanel;

    private void OnEnable()
    {
        JoinPanel.SetActive(true);
        networkManager = (NetworkManagerWander)NetworkManagerWander.singleton;
        NetworkManagerWander.OnClientConnected += HandleClientConnected;
        NetworkManagerWander.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerWander.OnClientConnected -= HandleClientConnected;
        NetworkManagerWander.OnClientDisconnected -= HandleClientDisconnected;
    }

    private void HandleClientConnected()
    {
        JoinPanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        
    }



    public void JoinLobby()
    {
        if(string.IsNullOrEmpty(ipAddressInputField.text))
        {
            networkManager.networkAddress = "localhost";
            Debug.Log("Starting Local client");
        }
        else
        {
            string ipAddress = ipAddressInputField.text;
            networkManager.networkAddress = ipAddress;
        }
        networkManager.StartClient();
    }
    public void HostLobby()
    {
        networkManager.StartHost();
        JoinPanel.SetActive(false);
        Debug.Log("Starting Local Host");
    }
}
