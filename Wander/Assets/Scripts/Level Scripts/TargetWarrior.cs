using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWarrior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("map00").GetComponent<ScrollBackground>();
        GameObject.Find("map01").GetComponent<ScrollBackground>();
        GameObject.Find("map02").GetComponent<ScrollBackground>();
        GameObject.Find("map03").GetComponent<ScrollBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
