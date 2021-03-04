using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX_Eye : MonoBehaviour
{



    //public functions to be called inside of the animations
    public void attackSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Eye/Attack", gameObject);
    }  
    public void damageSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Eye/Damage", gameObject);
    }
    public void deathSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Eye/Death", gameObject);
    }
    public void emoteSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Eye/Emote", gameObject);
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
