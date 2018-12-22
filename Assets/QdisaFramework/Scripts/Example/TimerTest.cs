/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 * 
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour {
    Timer timer;
	// Use this for initialization
	void Start () {
        timer = new Timer(5);
        timer.TimingEvent += SendMessage;
        timer.StartTimer();
        timer.ResetEndTimer(5);

    }
	
	// Update is called once per frame
	void Update () {
        
        timer.UpdateTimer(Time.deltaTime);
	}
    void SendMessage()
    {
        Debug.Log("计时器执行完成");
    }
    private void OnDestroy()
    {
        timer.TimingEvent -= SendMessage;
    }
}
