using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileSFX : MonoBehaviour
{

    // Start is called before the first frame update

    public void fireBallFlameSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Mage/Fireball", gameObject);
    }
    public void fireHitSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Mage/Fire Hit", gameObject);
    }
    public void fireStormSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Mage/Fire Storm", gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
