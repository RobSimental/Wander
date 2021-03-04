using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
class LobbyInformation{

	public string lobbyID;

	public string lobbyName;

	public string passwordRequired;

	public string playersId;

}
[System.Serializable]

class LobbyInformationList { 
	public LobbyInformation[] LobbyInformation;
}


public class Lobby : MonoBehaviour
{
    private GameObject mainObject;

    private MessageQueue msgQueue;

	private ConnectionManager cManager;

	private LobbyInformationList list;

	public GameObject ListButton;

	public bool isTrue = true;

	public List<GameObject> array = new List<GameObject>();

    void Start() {
		mainObject = GameObject.Find("MainObject");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_LOBBY, ResponseLobbies);
	}

	private void Update() {
		if(isTrue){
			cManager.send(requestLobbies());
			isTrue = false;
		}
	}
    public RequestLobbies requestLobbies(){
        RequestLobbies request = new RequestLobbies();
        request.send();
        return request;
    }

    public void ResponseLobbies(ExtendedEventArgs eventArgs) {
		ResponseLobbiesEventArgs args = eventArgs as ResponseLobbiesEventArgs;
		if (args.status == 0) {
			list = JsonUtility.FromJson<LobbyInformationList>(args.json);
			Debug.Log(args.json);
			for(int i = 0; i < list.LobbyInformation.Length; i++){
				GameObject listButton = Instantiate(ListButton);
				listButton.transform.SetParent(transform,false);
				Text[] ButtonTexts = listButton.GetComponentsInChildren<Text>();
				ButtonTexts[0].text = list.LobbyInformation[i].lobbyID;
				ButtonTexts[1].text = list.LobbyInformation[i].lobbyName;
				ButtonTexts[2].text = numberOfplayers(list.LobbyInformation[i].playersId) + "/3";
			}
		} 
	}
	public static int numberOfplayers(string s){
		int counter = 0;
		for(int i = 0; i < s.Length; i++){
			if(s[i] == ','){
				counter++;
			}
		}
		return counter + 1;
	}
	public void goToKey(){
		SceneManager.LoadScene("Wander(Prototype1)");
	}
}
