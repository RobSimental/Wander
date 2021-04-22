using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabRegistration : MonoBehaviour
{
    private string userEmail;
    private string userPassword;
    private string username;
    private string confirmPassword;
    public GameObject LoginPanel;
    public void OnClickLoadLogin()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Cancel");
        //SceneManager.LoadScene("Login");
        LoginPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void OnClickRegister()
    {
        if (userPassword != null && userPassword.Equals(confirmPassword))
        {
            //create request with success/failure functions
            var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Password Incorrect");
        }
    }
    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("User created"); 
        //save info for faster login
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Enter");
        //SceneManager.LoadScene("Login");
        LoginPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void OnRegisterFailure(PlayFabError error)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Password Incorrect");
        Debug.Log(error.GenerateErrorReport());
    }


    //functions used to set fields
    public void GetUserEmail(string e)
    {
        userEmail = e;
    }
    public void GetUserPassword(string p)
    {
        userPassword = p;
    }
    public void GetConfirmPassword(string c)
    {
        confirmPassword = c;
    }
    public void GetUsername(string u)
    {
        username = u;
    }
}
