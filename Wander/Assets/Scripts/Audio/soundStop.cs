using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundStop : StateMachineBehaviour
{
    private CharacterSFX_Warrior characterSFX_Warrior;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       characterSFX_Warrior = GameObject.FindObjectOfType<CharacterSFX_Warrior> ();
    }

   

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Input.GetButton("Ability3")){
            characterSFX_Warrior.move3SoundStop();
        }
        
    }

  
}
