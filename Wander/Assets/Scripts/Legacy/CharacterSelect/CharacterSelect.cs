using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject LobbySelect;
    public static string characterChoice;
    public void selectArcher()
    {
        characterChoice = "Archer";
    }

    public void selectMage()
    {
       characterChoice = "Mage";
    }

    public void selectWarrior()
    {
        characterChoice = "Warrior";
    }

    public void lobbySelect()
    {
        LobbySelect.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
