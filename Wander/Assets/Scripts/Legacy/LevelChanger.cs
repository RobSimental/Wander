using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class LevelChanger : NetworkBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public GameObject playerObject;
    public void FadeToLevel (int code)
    {
        levelToLoad = code;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        if (levelToLoad == 0) { SceneManager.LoadScene("GameOver");}
        else { SceneManager.LoadScene("Credits"); }
    }
}
