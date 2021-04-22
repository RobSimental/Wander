using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PressKey : MonoBehaviour
{
    public static string username;
    public GameObject CharacterSelect;
    public TMP_Text playDisplay;
    // Start is called before the first frame update
    private void Start() {
        playDisplay.text =  username;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            CharacterSelect.SetActive(true);
            this.gameObject.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/KeyPressed");
        }
    }
}
