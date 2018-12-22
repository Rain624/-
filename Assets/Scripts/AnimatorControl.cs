/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa.FSM;

public class AnimatorControl : MonoBehaviour {
    private Animator eleAnimator;

    // Use this for initialization
    void Start () {
        eleAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayIdle()
    {
        eleAnimator.SetBool("IsShake", false);
        eleAnimator.SetBool("IsPaint", false);
    }
    public  void PlayPaint()
    {
        eleAnimator.SetBool("IsPaint", true);
        eleAnimator.SetBool("IsShake", false);

    }
    public  void PlayShake()
    {
        eleAnimator.SetBool("IsShake", true);
    }
}


