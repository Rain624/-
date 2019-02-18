/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa;

public class ElephantAnimControl : MonoBehaviour {
    private Animator eleAnimator;

    // Use this for initialization
    void  OnEnable() {
        eleAnimator = GetComponent<Animator>();
        //eleAnimator.Play("Take 001");
	}

    private void Update()
    {
        Debug.Log(eleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"));
    }
    public void PlayWalk()
    {
        eleAnimator.SetBool("IsWalk", true);
        eleAnimator.SetBool("IsIdle", false);
        eleAnimator.SetBool("IsPaint", false);

    }
    public void PlayIdle()
    {
        eleAnimator.SetBool("IsIdle", true);
        eleAnimator.SetBool("IsPaint", false);
        eleAnimator.SetBool("IsWalk", false);

    }
    public  void PlayPaint()
    {
        eleAnimator.SetBool("IsPaint", true);
        eleAnimator.SetBool("IsWalk", false);
        eleAnimator.SetBool("IsIdle", false);

    }
    public  void PlayShake()
    {
        eleAnimator.SetBool("IsShake", true);
    }
    /// <summary>
    /// 回到行走的状态
    /// </summary>
    private void OnDisable()
    {
        
    }
}


