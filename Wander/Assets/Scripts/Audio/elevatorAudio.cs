using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string elevatorEvent;

    private FMOD.Studio.EventInstance elevator;

    // Start is called before the first frame update
    void Start()
    {
        elevator = FMODUnity.RuntimeManager.CreateInstance(elevatorEvent);
    }
    
    public Elevator platform;
    private bool isColliding;

    private void OnTriggerEnter2D(Collider2D other) {
        // if(Input.GetKey(KeyCode.R)) {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isColliding = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R) && isColliding) {
            elevator.start();
        // }
        }
    }
}
