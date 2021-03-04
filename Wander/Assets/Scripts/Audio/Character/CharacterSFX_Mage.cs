using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX_Mage : CharacterSFX
{
   

    public void castFireSound(){
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Mage/Cast Fire", gameObject);
    }

    void addSpecificAnimationEvent(){
        const float oneUnit = .125f;
        AnimationClip clip;
        Animator anim;
        AnimationEvent evt;
        evt = new AnimationEvent();
        anim = GetComponent<Animator>();

        //add cast to attack 1
        clip = anim.runtimeAnimatorController.animationClips[1];
        if (clip.empty){
            evt.time = 0f;
            evt.functionName = "castFireSound";
            clip.AddEvent(evt);
        }else {
            Debug.Log("Attack 1 already has animation");
        }
        //add cast to attack 2
        clip = anim.runtimeAnimatorController.animationClips[2];
        if(clip.empty){
            evt.functionName = "castFireSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("Attack 2 already has animation");
        }
        // add cast to attack 3
        clip = anim.runtimeAnimatorController.animationClips[3];
        if(clip.empty){
            evt.functionName = "castFireSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("Attack 3 already has animation");
        }
        //add cast to mage attack
        clip = anim.runtimeAnimatorController.animationClips[9];
        if(clip.empty){
            evt.functionName = "castFireSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("mage attack already has animation");
        }




        /*  clip array
            Number: 0 goes to clip: MageIdle (UnityEngine.AnimationClip)
            Number: 1 goes to clip: MageAbility1 (UnityEngine.AnimationClip)
            Number: 2 goes to clip: MageAbility2 (UnityEngine.AnimationClip)
            Number: 3 goes to clip: MageAbility3 (UnityEngine.AnimationClip)
        Number: 4 goes to clip: MageRun (UnityEngine.AnimationClip)
        Number: 5 goes to clip: MageJump (UnityEngine.AnimationClip)
            Number: 6 goes to clip: MageFall (UnityEngine.AnimationClip)
        Number: 7 goes to clip: MageLand (UnityEngine.AnimationClip)
        Number: 8 goes to clip: MageDeath (UnityEngine.AnimationClip)
            Number: 9 goes to clip: MageAttack (UnityEngine.AnimationClip)
        */
    }

     void Start(){


           // base.addAnimationEvents(4,5,7,8); //run, jump, land, death
            //addSpecificAnimationEvent();


    } 
}
