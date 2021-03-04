using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    public bool printFootstep = false;
    //public bool makeAnimationEvents = true;

    private float distance = 0.2f;
    private int Material= 1;

    public void footStepSound(){
        FMOD.Studio.EventInstance footStep = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Footsteps");
        footStep.setParameterByName("Surface", Material);
        footStep.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        footStep.start();
        if (printFootstep){
            float result;
            footStep.getParameterByName("Surface", out result);
            Debug.Log("The footstep surface param returns: " + result);
            printFootstep = false;
        }
        footStep.release();
        //Debug.Log("The footstep surface param returns: " + Material);
    }
    public void jumpSound(){
        FMOD.Studio.EventInstance jump = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Jump");
        jump.setParameterByName("Surface", Material);
        jump.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        jump.start();
        jump.release();
    }
    public void landSound(){
        FMOD.Studio.EventInstance land = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Landing");
        land.setParameterByName("Surface", Material);
        land.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        land.start();
        land.release();
    }
    public void deathSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/DeathStinger");
    }
    void MaterialCheck() // checks what material before footstep
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, distance, 1 << 8);

        if (hit.collider)
        {
            if (hit.collider.tag == "Material: Cave")
                Material = 0;
            else if (hit.collider.tag == "Material: Grass")
                Material = 1;
            else
                Material = 1;
        }
    }
    void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
    }

    public void addAnimationEvents(){
        addAnimationEvents(0,0,0,0);
    }

    public void addAnimationEvents(int run, int jump, int land, int death){
        const float oneUnit = .125f;
        AnimationClip clip;
        Animator anim;
        AnimationEvent evt;
        evt = new AnimationEvent();
        anim = GetComponent<Animator>();

        // add first footstep
        clip = anim.runtimeAnimatorController.animationClips[run]; // set clip to run animation
        if (clip.empty){
            evt.time = 1*oneUnit;
            evt.functionName = "footStepSound";
            clip = anim.runtimeAnimatorController.animationClips[run]; // set clip to run animation
            clip.AddEvent(evt);
            // add second footstep
            evt.time = 4*oneUnit;
            clip.AddEvent(evt);
        }else{
            Debug.Log("footstep animation wasn't empty");
        }

        //add jump
        clip = anim.runtimeAnimatorController.animationClips[jump];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "jumpSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("Jump animation wasn't empty");
        }

        //add land
        clip = anim.runtimeAnimatorController.animationClips[land];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "landSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("Land animation wasn't empty");
        }

        //add death stinger
        clip = anim.runtimeAnimatorController.animationClips[death];
        if(clip.empty){
            evt.time = 0f;
            evt.functionName = "deathSound";
            clip.AddEvent(evt);
        }else{
            Debug.Log("Death animation wasn't empty");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
