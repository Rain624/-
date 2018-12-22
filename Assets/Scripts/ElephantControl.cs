/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantControl : MonoBehaviour {
    public Action OnDisplay;
    [Range(0,100)]
    public float RotateSpeed=10.0f;


    private Vector3 elephantInitPos;
    private Quaternion elephantInitRot;

	// Use this for initialization
	void Start () {
        //GetInitEle();
   
    }

    // Update is called once per frame
    void Update () {
    }
    public void Rotate()
    {
        Tweener tweener=transform.DOLocalRotate(new Vector3(0, -720, 0), RotateSpeed, RotateMode.LocalAxisAdd);
        tweener.SetEase(Ease.InOutSine);
        tweener.onComplete = delegate ()
        {
            if (OnDisplay != null)
            {
                OnDisplay();
            }
            Debug.Log("事件完成");
        };

    }
    /// <summary>
    /// 获取大象的初始位置旋转
    /// </summary>
    private void GetInitEle()
    {
        elephantInitPos = transform.position;
        elephantInitRot = transform.rotation;
    }
}


