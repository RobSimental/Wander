using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;

public class StartUpLogic : MonoBehaviour
{
    public GameObject LoginPanel;
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            //set the title id to the Wander playfab title id
            PlayFabSettings.TitleId = "23BBA";
        }
        LoginPanel.SetActive(true);
    }
}
