using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PressKey : MonoBehaviour
{
    public GameObject CharacterSelect;
    public GameObject DisplayUsername;
    private TextMeshPro usernameText;

   private void Start() {
        //usernameText = DisplayUsername.GetComponent<TextMeshPro>();
        //usernameText.text = PlayFabLogin.username;
        Debug.Log(PlayFabLogin.username);
    }

    void Update()
    {
        if(Input.anyKeyDown){
            CharacterSelect.SetActive(true);
            this.gameObject.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/KeyPressed");
        }
    }
}
