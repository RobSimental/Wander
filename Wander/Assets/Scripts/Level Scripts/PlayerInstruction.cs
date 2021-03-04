using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstruction : MonoBehaviour
{
    public GameObject canvas;
    void Start() {
        canvas.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            canvas.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        canvas.SetActive(false);
    }
}
