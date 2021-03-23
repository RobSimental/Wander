using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayFabLogin : NetworkBehaviour
{
    private string userEmail;
    private string userPassword;
    public static string sessionTicket; //lets you know what player you are in server
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            //set the title id to the Wander playfab title id
            PlayFabSettings.TitleId = "23BBA";
        }
    } 

    //button function to open registration page
    public void OnClickLoadRegister() 
    {
        SceneManager.LoadScene("Registration");
    }
    //buttong function to attempt login
    public void OnClickLogin()
    {
        //create request with success/failure functions
        var loginRequest = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("logged in");
        sessionTicket = result.SessionTicket;
        SceneManager.LoadScene("KeyToContinue");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("failed to login");
        Debug.LogError(error.GenerateErrorReport());
    }

    //getter functions for private user info
    public void GetUserEmail(string e)
    {
        userEmail = e;
    }
    public void GetUserPassword(string p)
    {
        userPassword = p;
    }
}
