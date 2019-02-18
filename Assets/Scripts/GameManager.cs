/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa;
using System;


public class GameManager : MonoBehaviour,ISingleton
    {


        public NozzlePainter nozzlePainter;
        public TrackerControl trackerControl;
        public UIControl uIControl;
    [HideInInspector]
    public Transform StageTrans;
    [HideInInspector]
    public Transform StartPosTrans;
    [HideInInspector]
    public Transform EndPosTrans;
    [HideInInspector]
    public Transform ShowPosTrans;

    private Transform elephantTrans;
    private ElephantControl elephantControl;
    private   QdisaStateMachine elephantMachine;
    private QdisaState enterState;
    private QdisaState idleState;
    private QdisaState paintState;
    private QdisaState exitState;
    private QdisaTransition enter2idle;
    private QdisaTransition idle2paint;
    private QdisaTransition paint2exit;
    private QdisaTransition exit2enter;

    private bool is2Enter;
    private bool is2Idle;
    private bool is2Paint;
    private bool is2Exit;


    public bool Is2Enter
    {
        get
        {
            return is2Enter;
        }
        set
        {
            is2Enter = value;
        }
    }
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
  
    public bool Is2Exit
    {
        get
        {
            return is2Exit;
        }
        set
        {
            is2Exit = value;
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
    private void Awake()
    {
        StageTrans = GameObject.Find("Stage").transform;
        StartPosTrans = GameObject.Find("StartPos").transform;
        EndPosTrans = GameObject.Find("EndPos").transform;
        ShowPosTrans = GameObject.Find("ShowPos").transform;
        InitFsm();
    }


    private void Update()
    {
        elephantMachine.UpdateCallback(Time.deltaTime);
    }

    private  void InitFsm()
    {
        #region 进入画面的状态
        enterState = new QdisaState("Enter");
        enterState.OnEnter += (IState state) =>
          {
                  //生成大象
                  elephantTrans = PoolControl.Instance.SpawnElephant();
                  //大象到达预定位置
                  elephantControl = elephantTrans.GetComponent<ElephantControl>();

                  if (elephantControl != null)
                  {
                      elephantControl.OnEnterCompleted += SetIs2Idle;

                  }  
          };
        enterState.OnExit += (IState state) =>
          {
              Debug.Log("我在执行");
              Is2Enter = false;
              elephantControl.OnEnterCompleted -= SetIs2Idle;
          };
        #endregion

        #region 无人时的休闲状态
        idleState = new QdisaState("Idle");
        idleState.OnEnter += (IState state) =>
          {
              Debug.Log("我执行了吗");
              trackerControl.OnClick += SetIs2Paint;
              //进入无人状态
          };
        idleState.OnUpdate += (float f) =>
          {
              //无人状态做啥
              Debug.Log("我在无人状态");
          };
        idleState.OnExit += (IState state) =>
          {
              Is2Idle = false;
              //退出无人状态
              trackerControl.OnClick -= SetIs2Paint;
            
          };
        #endregion

        #region 喷涂状态
        paintState = new QdisaState("Paint");
        paintState.OnEnter += (IState state) =>
          {
              Debug.Log("我在喷涂状态");
              //大象动画停止并旋转展示
              elephantControl.RotateAction();
              //大象旋转完成则到退出状态
              elephantControl.OnDisplayCompleted +=SetIs2Exit;
              //喷枪运行
              trackerControl.OnClick += nozzlePainter.Spary;
              nozzlePainter.ColorEventHandle += uIControl.ChangeBorderColor;
              
            //添加大象的运动
              //elephantControl.OnDisplayCompleted += SetIs2Walk;
        };
        paintState.OnUpdate += (float f) =>
          {
              //喷绘状态做啥
              Debug.Log("我在喷绘状态");
          };
        paintState.OnExit += (IState state) =>
          {
              elephantTrans.parent = null;
              //退出喷绘状态
              Is2Paint = false;
              //喷枪结束
              trackerControl.OnClick -= nozzlePainter.Spary;
              nozzlePainter.ColorEventHandle -= uIControl.ChangeBorderColor;
              //取消大象的运动
              elephantControl.OnDisplayCompleted -=SetIs2Exit;
          };
        #endregion

        #region 退出状态
        exitState = new QdisaState("Exit");
        exitState.OnEnter += (IState state) =>
          {
              //进入褪色状态
              if (elephantControl != null)
              {
                  elephantControl.ExitShow();
                  //elephantControl.OnExitCompleted += () =>
                  //{
                  //    PoolControl.Instance.DespawnElephant(elephantTrans);
                  //};
              }
           

          };
        exitState.OnUpdate += (float f) =>
          {
              //褪色状态干啥
              SetIs2Enter();
          };
        exitState.OnExit += (IState state) =>
          {
              //退出褪色状态
              Is2Exit = false;
        };
        #endregion

        #region 状态过渡初始化
        //进入状态到站立状态
        enter2idle = new QdisaTransition("enter2idle", enterState, idleState);
        enter2idle.OnCheck += () =>
        {
            return Is2Idle;
        };
        enterState.AddTransition(enter2idle);

        //站立状态到喷绘状态
        idle2paint = new QdisaTransition("Idle2paint", idleState, paintState);
        idle2paint.OnCheck += () =>
        {
            return Is2Paint;
        };
        idleState.AddTransition(idle2paint);

        //喷涂状态到退出状态
        paint2exit = new QdisaTransition("paint2exit", paintState, exitState);
        paint2exit.OnCheck += () =>
        {
            return Is2Exit;
        };
        paintState.AddTransition(paint2exit);

        //退出状态到进入状态
        exit2enter = new QdisaTransition("exit2enter", exitState, enterState);
        exit2enter.OnCheck += () =>
        {
            return Is2Enter;
        };
        exitState.AddTransition(exit2enter);
        #endregion
        elephantMachine = new QdisaStateMachine("Root", exitState);

    }
    //转到休闲状态
    private void SetIs2Idle()
    {
        Is2Idle = true;
    }
    //转到喷涂状态
    private void SetIs2Paint()
    {
        Is2Paint = true;
    }
    private void SetIs2Exit()
    {
        Is2Exit = true;
    }
    private void SetIs2Enter()
    {
        Is2Enter = true;
    }

}




