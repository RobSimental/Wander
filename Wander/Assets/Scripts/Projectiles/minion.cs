using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion : MonoBehaviour
{
    Animator animator;
    public GameObject minionSumm;
    Vector2 minionPOS;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability2"))
        {

            SummonMin();

        }
    }
    void SummonMin() {

        minionPOS = transform.position;
        minionPOS += new Vector2(+0.3f, -0.043f);
        Instantiate(minionSumm, minionPOS, Quaternion.identity);
    }
}
