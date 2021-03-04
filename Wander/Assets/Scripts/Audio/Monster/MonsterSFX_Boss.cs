using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX_Boss : MonoBehaviour
{


    //public functions to be called inside of the animations
    public void chargeUpSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Charge Up", gameObject);
    }  
    public void damageSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Damage", gameObject);
    }
    public void deathSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Death", gameObject);
    }
    public void movementSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Movement", gameObject);
    }
    public void laserSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Laser", gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

