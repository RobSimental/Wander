﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
public class PlayFabRegistration : MonoBehaviour
{
    private string userEmail;
    private string userPassword;
    private string username;
    private string confirmPassword;
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            //set the title id to the Wander playfab title id
            PlayFabSettings.TitleId = "23BBA";
        }
    }
    public void OnClickLoadLogin()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Cancel");
        SceneManager.LoadScene("Login");
    }
    public void OnClickRegister()
    {
        if (userPassword.Equals(confirmPassword))
        {
            //create request with success/failure functions
            var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
        else
        {
            //todo make error visiable to user
            Debug.Log("passwords dont match");
        }
    }
    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("User created"); 
        //save info for faster login
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Enter");
        SceneManager.LoadScene("Login");
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