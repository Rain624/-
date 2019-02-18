/****************************************************************************
 *
 * Copyright (c) 2019 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseManager{
    protected GameManager gameManager;
    public BaseManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public virtual void OnInit()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestroy()
    {

    }
  

}


