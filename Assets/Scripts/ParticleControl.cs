/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class ParticleControl : MonoBehaviour {
    ParticleSystem particle;
    [SerializeField]
    private Brush brush = null;

    [SerializeField]
    private int wait = 3;

    private int waitCount;
    private void Start()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {

        if (waitCount < wait)
            return;
        waitCount = 0;

        foreach (var p in other.transform.GetComponent<Collision>().contacts)
        {
            var canvas = p.otherCollider.GetComponent<InkCanvas>();
            if (canvas != null)
                //canvas.Paint(brush, p.point);
                canvas.Erase(brush, p.point);
        }

    }



}


