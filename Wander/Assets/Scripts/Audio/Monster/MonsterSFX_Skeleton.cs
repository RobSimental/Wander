using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX_Skeleton : MonoBehaviour
{

    //public functions to be called inside of the animations
    public void attackSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Skeleton/Attack", gameObject);
    }  
    public void damageSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Skeleton/Damage", gameObject);
    }
    public void deathSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Skeleton/Death", gameObject);
    }
    public void footStepSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Footsteps", gameObject);
    }
    public void emoteSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Skeleton/Emote", gameObject);
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
