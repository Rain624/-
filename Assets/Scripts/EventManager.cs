/****************************************************************************
 *
 * Copyright (c) 2019 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qdisa;


[QMonoSingletonPath("EventManager")]
public class EventManager : MonoBehaviour,ISingleton {
    public event Action OnEnter;
    public  Action OnEnterCompleted;
    //public event Action OnIdleCompleted;
    //public event Action OnPaintCompleted;
    //public event Action OnExitCompleted;
    public EventManager()
    {

    }
    public static EventManager Instance
    {
        get
        {
            return MonoSingletonProperty<EventManager>.Instance;
        }
    }
    public void Dispose()
    {
        MonoSingletonProperty<EventManager>.Dispose();
    }
    //public EventManager(GameManager gameManager) : base(gameManager)
    //{

    //}

}


