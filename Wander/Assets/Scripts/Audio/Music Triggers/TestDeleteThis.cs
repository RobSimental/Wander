using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDeleteThis : MonoBehaviour
{
    MusicManager music;
    public bool changeMusic= false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject GO = GameObject.Find("Music");
        music = (MusicManager) GO.GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(changeMusic){
            music.menuMusic(0);
        }
        if(!changeMusic){
            music.bossMusic(0);
        }
    }
}
