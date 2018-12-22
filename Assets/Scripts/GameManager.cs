/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa.Singleton;
using Qdisa.FSM;
using System;

public class GameManager : MonoBehaviour,ISingleton
    {
   
        public AnimatorControl animatorControl;
        public ElephantControl elephantControl;
        public NozzlePainter nozzlePainter;
    public TrackerControl trackerControl;
    private QdisaStateMachine elephantMachine;
    private QdisaState idleState;
    private QdisaState paintState;
    private QdisaState shakeState;
    private QdisaTransition idle2paint;
    private QdisaTransition paint2idle;
    private QdisaTransition paint2shake;
    private QdisaTransition shake2paint;

    private bool is2Paint;
    private bool is2Idle;
    private bool is2Shake;

    public bool Is2Paint
    {
        get
        {
            return is2Paint;
        }
        set
        {
            is2Paint = value;
        }
    }
    public bool Is2Idle
    {
        get
        {
            return is2Idle;
        }
        set
        {
            is2Idle = value;
        }
    }
    public bool Is2Shake
    {
        get
        {
            return is2Shake;
        }
        set
        {
            is2Shake = value;
        }
    }
    public GameManager()
    {

    }
    public static GameManager Instance
    {
        get
        {
            return MonoSingletonProperty<GameManager>.Instance;
        }
    }
    public void Dispose()
    {
        MonoSingletonProperty<GameManager>.Dispose();
    }
    private void Start()
    {
        InitFsm();
    }


    private void Update()
    {
        elephantMachine.UpdateCallback(Time.deltaTime);
    }

    private  void InitFsm()
    {
          trackerControl.OnClick+=SetIs2Paint;
        idleState = new QdisaState("Idle");
        idleState.OnEnter += (IState state) =>
          {
              trackerControl.OnClick += SetIs2Paint;
              animatorControl.PlayIdle();
              //进入无人状态
          };
        idleState.OnUpdate += (float f) =>
          {
              //无人状态做啥
              Debug.Log("我在无人状态");
          };
        idleState.OnExit += (IState state) =>
          {
              //退出无人状态
              trackerControl.OnClick -= SetIs2Paint;

          };
        paintState = new QdisaState("Paint");
        paintState.OnEnter += (IState state) =>
          {

              //进入绘画状态
              //大象动画停止
              animatorControl.PlayPaint();
              //大象开始旋转
              elephantControl.Rotate();

              //喷枪运行
              trackerControl.OnClick += nozzlePainter.Spary;
            //添加大象的运动
              elephantControl.OnDisplay += SetIs2Shake;
        };
        paintState.OnUpdate += (float f) =>
          {
              //喷绘状态做啥
              Debug.Log("我在喷绘状态");
          };
        paintState.OnExit += (IState state) =>
          {
              //退出喷绘状态
              Is2Paint = false;
              //喷枪结束
              trackerControl.OnClick -= nozzlePainter.Spary;
              //取消大象的运动
              elephantControl.OnDisplay -= SetIs2Shake;
          };
        shakeState = new QdisaState("Shake");
        shakeState.OnEnter += (IState state) =>
          {
              //进入褪色状态
          };
        shakeState.OnUpdate += (float f) =>
          {
              //褪色状态干啥
          };
        shakeState.OnExit += (IState state) =>
          {
            //退出褪色状态
        };
        #region 状态过渡初始化
        //站立状态到喷绘状态
        idle2paint = new QdisaTransition("Idle2paint", idleState, paintState);
        idle2paint.OnCheck += () =>
        {
            return Is2Paint;
        };
        idleState.AddTransition(idle2paint);

        //喷绘状态到站立状态
        paint2idle = new QdisaTransition("Paint2idle", paintState, idleState);
        paint2idle.OnCheck += () =>
        {
            return Is2Idle;
        };
        paintState.AddTransition(paint2idle);

        //喷绘状态到褪色状态
        paint2shake = new QdisaTransition("Paint2shake", paintState, shakeState);
        paint2shake.OnCheck += () =>
        {
            return Is2Shake;
        };
        paintState.AddTransition(paint2shake);

        //褪色状态到喷绘状态
        shake2paint = new QdisaTransition("Shake2paint", shakeState, paintState);
        shake2paint.OnCheck += () =>
        {
            return Is2Paint;
        };
        shakeState.AddTransition(shake2paint);

        #endregion
        elephantMachine = new QdisaStateMachine("Root", idleState);

    }
    private void SetIs2Paint()
    {
        Is2Paint = true;
    }
    private void SetIs2Shake()
    {
        Is2Shake = true;
    }
}




