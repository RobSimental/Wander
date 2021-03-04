using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger2d : musicTrigger
{


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.Find("MusicTag").tag == "LocalPlayer"){
            base.setMusic();
        }
    }
}
