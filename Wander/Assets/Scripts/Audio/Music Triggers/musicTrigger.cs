using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger : MonoBehaviour
{
    MusicManager music;
    public musicChoices musicChoice;
    public bool changeMusic = false;
    
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
                music.menuMusic(0);
        }
        if (musicChoice == musicChoices.characterSelect){
                music.menuMusic(1);
        }
        if (musicChoice == musicChoices.cave){
                music.caveMusic();
        }
        if (musicChoice == musicChoices.forest1){
                music.forestMusic(0);
        }
        if (musicChoice == musicChoices.forest2){
                music.forestMusic(1);
        }
        if (musicChoice == musicChoices.boss1){
                music.bossMusic(0);
        }
        if (musicChoice == musicChoices.boss2){
                music.bossMusic(1);
        }
        if (musicChoice == musicChoices.bossDeath){
                music.bossMusic(2);
        }
        if (musicChoice == musicChoices.credit){
                music.creditMusic();
        }
        if (musicChoice == musicChoices.gameOver){
                music.deathMusic();
        }
        if (musicChoice == musicChoices.noMusic){
                music.noMusicFullReset();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject GO = GameObject.Find("Music");
        music = (MusicManager) GO.GetComponent<MusicManager>();
        
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
