using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;


    public delegate void ClickEventHandler();

    public class TrackerControl : MonoBehaviour
    {
        public event ClickEventHandler OnClick;


        private  SteamVR_TrackedObject trackedObject;
        private  SteamVR_Controller.Device device;

        //public Text text;

        void Start()
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            device = SteamVR_Controller.Input((int)trackedObject.index);
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)||Input.GetKey(KeyCode.Space))
            {
                if (OnClick != null)
                    OnClick();
                Debug.Log("Trigger被按下");
            }
        //if (device.GetAxis(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    Debug.Log("我轻轻按了他");
        //}
        }
    }

