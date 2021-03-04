using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Registration : MonoBehaviour
{   
    private GameObject mainObject;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmedPasswordField;
    private string username = "";
	private string password = "";
	private string confirmedPassword = "";

    private MessageQueue msgQueue;
	private ConnectionManager cManager;

    public Button singUpButton;
    
    void Start() {
        mainObject = GameObject.Find("MainObject");
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        if(!msgQueue.callbackList.ContainsKey(Constants.SMSG_REG)){
			msgQueue.AddCallback(Constants.SMSG_REG, ResponseRegistration);
		}
    }

    public void Submit(){
        username = usernameField.text;
        password = passwordField.text;
        confirmedPassword = confirmedPasswordField.text;
		username = username.Trim();
		password = password.Trim();
        confirmedPassword = confirmedPassword.Trim();
        if(username.Length == 0){
            Debug.Log("User ID Required");
        } else if (password.Length == 0 || confirmedPassword.Length == 0){
            Debug.Log("Password Required");
        } else {
            cManager.send(requestRegistration(username, password, confirmedPassword));
        }
    }

    public void goToLogin(){
        SceneManager.LoadScene("Login");
    }

    public void ResponseRegistration(ExtendedEventArgs eventArgs) {
		ResponseRegistrationEventArgs args = eventArgs as ResponseRegistrationEventArgs;
		if (args.status == 0) {
			Debug.Log ("Successful Registration response : " + args.ToString());
            Debug.Log("----------------------------");
            SceneManager.LoadScene("Login");
		} else {
			Debug.Log("Registration Failed");
		}
	}

    public RequestRegistration requestRegistration(string username, string password, string confirmedPassword){
        RequestRegistration request = new RequestRegistration();
        request.send(username,password,confirmedPassword);
        return request;
    }
}