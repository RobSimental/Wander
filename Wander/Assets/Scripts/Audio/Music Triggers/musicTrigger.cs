using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger : MonoBehaviour
{
    public MusicManager musicManager;
    public musicChoices musicChoice;
    public bool changeMusic = false;
    private GameObject mm;
    private void Start()
    {
        mm = GameObject.Find("MusicManager");
        musicManager = mm.GetComponent<MusicManager>();
    }
    public enum musicChoices{ 
        menuMusic,
        characterSelect,
        cave,
        forest1,
        forest2,
        boss1,
        boss2,
        bossDeath,
        credit,
        gameOver,
        noMusic
    };
    public void setMusic(){
        if (musicChoice == musicChoices.menuMusic){
            musicManager.menuMusic(0);
        }
        if (musicChoice == musicChoices.characterSelect){
            musicManager.menuMusic(1);
        }
        if (musicChoice == musicChoices.cave){
            musicManager.caveMusic();
        }
        if (musicChoice == musicChoices.forest1){
            musicManager.forestMusic(0);
        }
        if (musicChoice == musicChoices.forest2){
            musicManager.forestMusic(1);
        }
        if (musicChoice == musicChoices.boss1){
            musicManager.bossMusic(0);
        }
        if (musicChoice == musicChoices.boss2){
            musicManager.bossMusic(1);
        }
        if (musicChoice == musicChoices.bossDeath){
            musicManager.bossMusic(2);
        }
        if (musicChoice == musicChoices.credit){
            musicManager.creditMusic();
        }
        if (musicChoice == musicChoices.gameOver){
            musicManager.deathMusic();
        }
        if (musicChoice == musicChoices.noMusic){
            musicManager.noMusicFullReset();
        }
    }


    // Update is called once per frame
    void Update()
    {
            if (!changeMusic){
                setMusic();
                changeMusic = true;
            }
    }
}
