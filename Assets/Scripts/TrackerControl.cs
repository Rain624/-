using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR;
using HTC.UnityPlugin.Vive;


    public delegate void ClickEventHandler();

    public class TrackerControl : MonoBehaviour
    {
        public event ClickEventHandler OnClick;


        //private  SteamVR_TrackedObject trackedObject;
        //private  SteamVR_Controller.Device device;

        void Start()
        {
            //trackedObject = GetComponent<SteamVR_TrackedObject>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            Debug.Log("Trigger被按下");

            if (OnClick != null)
                OnClick();
        }
        //device = SteamVR_Controller.Input((int)trackedObject.index);
        //if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)||Input.GetKey(KeyCode.Space))
        //{
        //Debug.Log("Trigger被按下");

        //if (OnClick != null)
        //        OnClick();
        //}

    }
    }

