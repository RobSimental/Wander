using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX_Ranger : CharacterSFX
{
    public void attackSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Archer/Basic Attack", gameObject);
    }  
    public void arrowVolleySound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Archer/Arrow Volley", gameObject);
    }
    public void redArrowSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Archer/Red Arrow", gameObject);
    }
    public void rollingAttackSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Archer/Rolling Attack", gameObject);
    }

    void addSpecificAnimationEvent(){
        const float oneUnit = .125f;
        AnimationClip clip;
        Animator anim;
        AnimationEvent evt;
        evt = new AnimationEvent();
        anim = GetComponent<Animator>();

        //add attack
        clip = anim.runtimeAnimatorController.animationClips[5];
        if (clip.empty){
            evt.time = 0f;
            evt.functionName = "attackSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("attack animation already has events");
        }

        //add abilty1  THIS ONE ISN'T SET UP YET
        clip = anim.runtimeAnimatorController.animationClips[6];
        evt.time = 0f;
        evt.functionName = "attackSound";
        clip.AddEvent(evt);


        /*  clip array
            Number: 0 goes to clip: RangerIdle (UnityEngine.AnimationClip)
        Number: 1 goes to clip: RangerJump (UnityEngine.AnimationClip)
            Number: 2 goes to clip: RangerFall (UnityEngine.AnimationClip)
        Number: 3 goes to clip: RangerLand (UnityEngine.AnimationClip)
        Number: 4 goes to clip: RangerRun (UnityEngine.AnimationClip)
            Number: 5 goes to clip: RangerAttack (UnityEngine.AnimationClip)
            Number: 6 goes to clip: RangerAbility1 (UnityEngine.AnimationClip)
        */
    }

     void Start(){

            //base.addAnimationEvents(4,1,3,2); //run, jump, land, death *DEATH VALUE IS temporary
            //addSpecificAnimationEvent();


    } 
}
