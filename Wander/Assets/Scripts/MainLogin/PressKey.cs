using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PressKey : MonoBehaviour
{

    public TMP_Text playDisplay;
    // Start is called before the first frame update
    private void Start() {
        if(DBManager.LoggedIn) {
            playDisplay.text = "Player: " + DBManager.username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/KeyPressed");
        }
    }
}
