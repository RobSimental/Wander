using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject LobbySelect;
    public void selectArcher()
    {
        DBManager.choosen = "Archer";
    }

    public void selectMage()
    {
        DBManager.choosen = "Mage";
    }

    public void selectWarrior()
    {
        DBManager.choosen = "Warrior";
    }

    public void lobbySelect()
    {
        LobbySelect.SetActive(true);
        this.gameObject.SetActive(false);
        //SceneManager.LoadScene("Wander(LevelProto)");
    }
}
