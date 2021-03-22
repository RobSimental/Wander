using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Mirror;


public class Login : MonoBehaviour
{
    private GameObject mainObject;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    private string username = "";
	private string password = "";

	private MessageQueue msgQueue;
	private ConnectionManager cManager;

	/*public void Submit()
    {
        //Todo call the server command to validate login
        //Submit is called by the login button, the client then asks the server to validate the login

    }

	//command specifices this function to be only callable by the client and only executable by the server
	//cheaters cant log bypass login this way
	//[Command]
	*//*public void LoginUser()
    {
		PlayfabClientAPI.LoginWithPlayFav()
    }*/
    
  
    void Awake()
    {
        mainObject = GameObject.Find("MainObject");
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        if (!msgQueue.callbackList.ContainsKey(Constants.SMSG_AUTH))
        {
            msgQueue.AddCallback(Constants.SMSG_AUTH, ResponseLogin);
        }
    }






    public void Submit()
    {
        username = usernameField.text;
        password = passwordField.text;
        username = username.Trim();
        password = password.Trim();
        if (username.Length == 0)
        {
            Debug.Log("User ID Required");
        }
        else if (password.Length == 0)
        {
            Debug.Log("Password Required");
        }
        else
        {
            cManager.send(requestLogin(username, password));
        }
    }

    public RequestLogin requestLogin(string username, string password)
    {
        RequestLogin request = new RequestLogin();
        request.send(username, password);
        return request;
    }

    public void ResponseLogin(ExtendedEventArgs eventArgs)
    {
        ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;
        if (args.status == 0)
        {
            Constants.USER_ID = args.user_id;
            Debug.Log("Successful Login response : " + args.ToString());
            DBManager.username = username;
            DBManager.id = Constants.USER_ID;
            SceneManager.LoadScene("KeyToContinue");
        }
        else
        {
            Debug.Log("Login Failed");
        }
    }

    public void goToRegister()
    {
        SceneManager.LoadScene("Registration");
    }
}
