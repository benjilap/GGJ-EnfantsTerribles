using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [HideInInspector]
    public bool setTransition;

    Animator myAtor;

    void Start()
    {
        myAtor = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        myAtor.SetBool("PlayerDead", setTransition);

    
    }
}
