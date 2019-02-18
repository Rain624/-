/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using DG.Tweening;
using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantControl : MonoBehaviour
{
    public event Action OnEnterCompleted;     //到达展示的指定位置
    public event Action OnDisplayCompleted;  //展示完成
    public event Action OnExitCompleted;         //退出画面完成

    [Range(0, 100)]
    [SerializeField]
    private float RotateTime = 10.0f;
    [Range(0, 30)]
    [SerializeField]
    private float MoveTime = 10f;
    [Range(0,10)]
    [SerializeField]
    private float ToDisplayTime = 10f; 
    private ElephantAnimControl elephantAnimControl;
    private  Transform StageTrans;
    private Transform StartPosTrans;
    private  Transform EndPosTrans;
    private  Transform ShowPosTrans;

    //private Vector3 elephantInitPos;
    //private Quaternion elephantInitRot;


    // Use this for initialization
    void OnEnable()
    {
        //StageTrans = GameObject.Find("Stage").transform;
        //StartPosTrans = GameObject.Find("StartPos").transform;
        //EndPosTrans = GameObject.Find("EndPos").transform;
        //ShowPosTrans = GameObject.Find("ShowPos").transform;
        StageTrans = GameManager.Instance.StageTrans;
        StartPosTrans = GameManager.Instance.StartPosTrans;
        EndPosTrans = GameManager.Instance.EndPosTrans;
        ShowPosTrans = GameManager.Instance.ShowPosTrans;
        elephantAnimControl = transform.GetComponent<ElephantAnimControl>();
        EnterShow();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnterShow();
        }
    }
    /// <summary>
    /// 大象进入到展示区域
    /// </summary>
    public void EnterShow()
    {
        //大象播放走路动画
        elephantAnimControl.PlayWalk();
        if (transform.position != StartPosTrans.position)
            transform.position = StartPosTrans.position;
        //大象移动到展示区域
        Tweener tweener = transform.DOMove(ShowPosTrans.position, MoveTime);
        tweener.SetEase(Ease.Linear);
        tweener.onComplete = delegate ()
        {
            IdleShow();
            //舞台转向较好的角度
            Tweener Stagetween = StageTrans.DOLocalRotate(new Vector3(0, -30, 0), ToDisplayTime, RotateMode.LocalAxisAdd);
            Stagetween.SetEase(Ease.InOutSine);
            Stagetween.onComplete = delegate ()
            {
                //到达展示位置
                if (OnEnterCompleted != null)
                {
                    OnEnterCompleted();
                }
                Debug.Log("转动完成可以进行喷涂了");
            };

        };
    }
    /// <summary>
    /// 无人状态大象播放待机动画
    /// </summary>
    public void IdleShow()
    {
        //大象播放待机动画
        elephantAnimControl.PlayIdle();

    }
    /// <summary>
    /// 喷涂阶段的旋转
    /// </summary>
    public void RotateAction()
    {
        //大象旋转状态
        elephantAnimControl.PlayPaint();
        Tweener tweener = StageTrans.DOLocalRotate(new Vector3(0, -720, 0), RotateTime, RotateMode.LocalAxisAdd);
        tweener.SetEase(Ease.InOutSine);
        tweener.onComplete = delegate ()
        {
            if (OnDisplayCompleted != null)
            {
                OnDisplayCompleted();
            }
            Debug.Log("事件完成");
        };

    }

    /// <summary>
    /// 退出到展示区域并销毁
    /// </summary>
    public void ExitShow()
    {
        //大象播放走路动画
        elephantAnimControl.PlayWalk();
        //大象移动到画面外
        Tweener tweener = transform.DOMove(EndPosTrans.position, MoveTime);
        tweener.SetEase(Ease.Linear);
        tweener.onComplete = delegate ()
        {
            ////到达退出位置
            //if (OnExitCompleted != null)
            //{
            //    OnExitCompleted();
            //}
           var paintObject = GetComponentInChildren<InkCanvas>();
            paintObject.ResetPaint();
            PoolControl.Instance.DespawnElephant(this.gameObject.transform);
        };
    }
}



