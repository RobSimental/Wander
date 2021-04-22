using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobbySelect : MonoBehaviour
{
   public void OnClickJoin()
    {
        SceneManager.LoadScene("Wander(LevelProto)");
    }
}
