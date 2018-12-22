/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 * 
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    private bool isTiming;
    private float curTime;
    private float endTime;
   
    public delegate void TimeDelegate();

    public event TimeDelegate TimingEvent;

    public Timer(float second )
    {
        curTime = 0;
        endTime = second;
    }
    public   void StartTimer()
    {
        isTiming = true;
    }
    public void UpdateTimer(float deltaTime)
    {
        if (isTiming)
        {
            curTime += deltaTime;
            if (curTime >= endTime)
            {
                isTiming = false;
                TimingEvent();
            }
        }
    }
    private void StopTimer()
    {
        isTiming = false;
    }
    public void ContinueTimer()
    {
        isTiming = true;
    }
    public void RestartTimer()
    {
        isTiming = true;
        curTime = 0;
    }
    public void ResetEndTimer(float second)
    {
        endTime = second;
        StartTimer();
    }
}
