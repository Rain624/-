/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    public Image BorderUI;
	
    public  void ChangeBorderColor(Color color)
    {
        BorderUI.color = color;
    }
}


