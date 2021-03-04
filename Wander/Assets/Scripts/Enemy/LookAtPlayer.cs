using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public bool isFlipped = false;

    private void OnTriggerEnter2D (Collider2D other) {
        if(other.tag == "Player") {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        // if(other.tag == "Player") {
        //     player = null;
        // }
    }
    public void lookAtPlayer() {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped) {
            Debug.Log("flip is working ONE");
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        } else if (transform.position.x > player.transform.position.x && !isFlipped) {
            Debug.Log("flip is working TWO");
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
