using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using Mirror;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayFabLogin : NetworkBehaviour
{
    public static string username;
    //todo probably shouldnt save passwords locally since not secure, but allows for easier development
    public GameObject emailInput;
    public GameObject passwordInput;
    public GameObject registrationPanel;
    public GameObject pressKey;
    private TMP_InputField emailTextField;
    private TMP_InputField passwordTextField;
    private string userEmail;
    private string userPassword;
    public static string sessionTicket; //lets you know what player you are in server
    // Start is called before the first frame update
    void Start()
    {
 
        emailTextField = emailInput.GetComponent<TMP_InputField>();
        passwordTextField = passwordInput.GetComponent<TMP_InputField>();
        //get player preferences for easy login
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            emailTextField.text = PlayerPrefs.GetString("EMAIL");
            passwordTextField.text = PlayerPrefs.GetString("PASSWORD");
        }
    } 

    //button function to open registration page
    public void OnClickLoadRegister() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Enter");    
        registrationPanel.SetActive(true);
        this.gameObject.SetActive(false);
        //SceneManager.LoadScene("Registration");
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
 



        //Playfab API not returning the username for some reason, should work on paper
        //PressKey.username = result.InfoResultPayload.AccountInfo.TitleInfo.DisplayName;
        sessionTicket = result.SessionTicket;
        //save player preferences locally
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Enter");

        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest { Email = userEmail }, accountResult =>
        {
            username = accountResult.AccountInfo.Username;
            Debug.Log(accountResult.AccountInfo.Username);
            pressKey.SetActive(true);
            this.gameObject.SetActive(false);
        }, accountError => { Debug.Log(accountError.GenerateErrorReport()); });

    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("failed to login");
        Debug.LogError(error.GenerateErrorReport());
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Password Incorrect");
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
