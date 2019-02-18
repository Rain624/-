/****************************************************************************
 *
 * Copyright (c) 2019 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NozzleImage : MonoBehaviour {

    public NozzlePainter nozzlePainter;
    public Texture2D image;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.O))
        {
            nozzlePainter.brush.BrushTexture = image;
            nozzlePainter.brush.Color = Color.white;
            nozzlePainter.brush.ColorBlending = Es.InkPainter.Brush.ColorBlendType.UseBrush;
        }
	}
}
