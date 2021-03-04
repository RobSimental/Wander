using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
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

    public void changeSceneToNext()
    {
        SceneManager.LoadScene("Wander(Prototype1)");
    }
}
