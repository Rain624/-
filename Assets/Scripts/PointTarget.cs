/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTarget : MonoBehaviour {
    [SerializeField]
    private float defaultDistance = 5f;
    [SerializeField]
    private bool useNormal;
    [SerializeField]
    private Image pointImage;
    [SerializeField]
    private Transform pointTransform;
    [SerializeField]
    private Transform Nozzle;

    private Vector3 originalScale;
    private Quaternion originalRotation;

    public bool UseNormal
    {
        get
        {
            return useNormal;
        }
        set
        {
            useNormal = value;
        }
    }
    public  Transform PointTransform
    {
        get
        {
            return PointTransform;
        }
    }
	// Use this for initialization
	void Start ()
    {
        originalScale = pointTransform.localScale;
        originalRotation = pointTransform.localRotation;
	}
    public void Hide()
    {
        pointImage.enabled = false;
    }
	public void Show()
    {
        pointImage.enabled = true;
    }
    public void SetPosition()
    {
        pointTransform.position = Nozzle.position + Nozzle.up * defaultDistance;
        pointTransform.localScale = originalScale*defaultDistance/1.5f ;
        pointTransform.localRotation = originalRotation;

    }
    public void SetPosition(RaycastHit hit)
    {
        if (hit.transform.gameObject.layer == 8)
            pointTransform.position = hit.point - transform.TransformDirection(Vector3.up*0.1f);
        else
            pointTransform.position = hit.point;
        if (hit.transform.CompareTag("ColorPail"))
            pointTransform.localScale = originalScale * hit.distance / 1.5f;
        else
            pointTransform.localScale = originalScale * hit.distance;
        if (useNormal)
        {
            pointTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        }
        else
        {
            pointTransform.localRotation = originalRotation;
        }
    }
    public void ShotRay()
    {
        Ray ray = new Ray(Nozzle.position, Nozzle.up);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 100))
        {
            SetPosition(hit);
        }
        else
        {
            SetPosition();
        }
    }
    private void Update()
    {
        ShotRay();
    }
}


