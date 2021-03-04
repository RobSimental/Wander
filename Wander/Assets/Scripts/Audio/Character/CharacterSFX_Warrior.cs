using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX_Warrior : CharacterSFX
{

    private FMOD.Studio.EventInstance move3;


    //public functions to be called inside of the animations
    public void attackSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Warrior/Basic Attack");
    }  
    public void move1Sound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Warrior/Move1");
    }
    public void move2Sound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Warrior/Move2");
    }
    public void move3SoundStart(){ //starts event and starts the held sound
        move3.start();
        move3.setParameterByName("Shield Hold", 1);

    }
    public void move3SoundStop(){  //releases held sound
        move3.setParameterByName("Shield Hold", 0);
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
        if(clip.empty){
            evt.time = 1*oneUnit;
            evt.functionName = "attackSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("attack already has animation");
        }

        //add move1
        clip = anim.runtimeAnimatorController.animationClips[9];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "move1Sound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("move 1 already has animation");
        }

        //add move2
        clip = anim.runtimeAnimatorController.animationClips[8];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "move2Sound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("move 2 already has animation");
        }

        //add move3 start
        clip = anim.runtimeAnimatorController.animationClips[6];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "move3SoundStart";
            clip.AddEvent(evt);
        }else{
            Debug.Log("move 3 already has animation");
        }




        /*  clip array
        Number: 0 goes to clip: Warrior_Run (UnityEngine.AnimationClip)
            Number: 1 goes to clip: Warrior_Idle (UnityEngine.AnimationClip)
        Number: 2 goes to clip: Warrior_Jump (UnityEngine.AnimationClip)
            Number: 3 goes to clip: Warrior_Fall (UnityEngine.AnimationClip)
        Number: 4 goes to clip: Warrior_Land (UnityEngine.AnimationClip)
            Number: 5 goes to clip: Warrior_Attack (UnityEngine.AnimationClip)
            Number: 6 goes to clip: Warrior_Ability3 (UnityEngine.AnimationClip)
            Number: 7 goes to clip: Warrior_Ability3_2 (UnityEngine.AnimationClip)
            Number: 8 goes to clip: Warrior_Ability2 (UnityEngine.AnimationClip)
            Number: 9 goes to clip: Warrior_Ability1 (UnityEngine.AnimationClip)
            Number: 10 goes to clip: Warrior_Death (UnityEngine.AnimationClip)

        */
    }

     void Start(){

        //base.addAnimationEvents(0,2,4, 10); //run, jump, land, death
        //addSpecificAnimationEvent();


        move3 = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Warrior/Move3");
        
    }

}



