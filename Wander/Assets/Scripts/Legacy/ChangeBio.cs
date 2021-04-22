using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBio : MonoBehaviour
{
    public Text title = null;
    public Text bio = null;

    public void changeTitle (string newTitle)
    {
        title.text = newTitle;
    }

    public void changeBio (string newBio)
    {
        bio.text = newBio;
    }
}
