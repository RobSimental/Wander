using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetAnimationOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        creatText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void creatText(){
        string path = Application.dataPath + "/AnimationLog.txt";

        if (!File.Exists(path)){
            File.WriteAllText(path, "Animation Log \n\n");
        }
        string content;

        AnimationClip clip;
        Animator anim;
        AnimationEvent evt;
        evt = new AnimationEvent();
        anim = GetComponent<Animator>();

        int i =0;
        while (anim.runtimeAnimatorController.animationClips[i]!= null){
            clip = anim.runtimeAnimatorController.animationClips[i];
            content = "Number: " + i + " goes to clip: " + clip + "\n";
            File.AppendAllText(path, content);
            i++;
        }


    }
}
