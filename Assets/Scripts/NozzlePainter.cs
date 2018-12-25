/****************************************************************************
 *
 * Copyright (c) 2018 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using System;



public class NozzlePainter : MonoBehaviour
    {
    public Action<Color> ColorEventHandle;
    public Transform Nozzle;
        [System.Serializable]
        private enum UseMethodType
        {
            RaycastHitInfo,
            WorldPoint,
            NearestSurfacePoint,
            DirectUV,
        }

        [SerializeField]
        public Brush brush;

        [SerializeField]
        private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

        [SerializeField]
        private bool erase = false;

        /// <summary>
        /// 颜色变化的快慢，数字越大变化越慢
        /// </summary>
        private  readonly float duration = 10.0f;
        [Range(0,1)]
        public float a;
        private void Awake()
        {
        }
        private void Update()
        {
            //ColorChange();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Reset();
            }
        }
        public void Spary()
        {
            bool success = true;
            RaycastHit hitInfo;
        Debug.DrawRay(Nozzle.position, Nozzle.TransformDirection(Vector3.up), Color.white);

        if (Physics.Raycast(Nozzle.position, Nozzle.TransformDirection(Vector3.up), out hitInfo, 100))
            {
            ColorChange(hitInfo.transform);
                var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                if (paintObject != null)
                {

                    switch (useMethodType)
                    {
                        case UseMethodType.RaycastHitInfo:
                            success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                            break;

                        case UseMethodType.WorldPoint:
                            success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
                            break;

                        case UseMethodType.NearestSurfacePoint:
                            success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                            break;

                        case UseMethodType.DirectUV:
                            if (!(hitInfo.collider is MeshCollider))
                                Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                            success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
                            break;
                    }
                }


                if (!success)
                    Debug.LogError("Failed to paint.");
            }
        }

    /// <summary>
    /// 颜色彩虹渐变
    /// </summary>
    private void ColorChange()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        brush.Color = Color.HSVToRGB(lerp, 1, 1);
    }
    /// <summary>
    /// 获取点击的色桶的颜色
    /// </summary>
    /// <param name="colorPail">颜色桶</param>
    private void ColorChange(Transform colorPail)
    {
        if (colorPail.gameObject.layer ==9)
        {
            Color color = colorPail.GetComponent<MeshRenderer>().material.color;
            brush.Color = color;
            if (ColorEventHandle != null)
            {
                ColorEventHandle(color);
            }
        }
     

    }
    ///// <summary>
    ///// 颜色擦除
    ///// </summary>
    //private void ColorErase()
    //    {
    //        for(int index=0;index<100;index++)
    //        {
    //            Vector3 pos = new Vector3(cube.position.x, cube.position.y + index * a, cube.position.z - index
    //                * a);
    //            var paintObject = elephant.GetComponent<InkCanvas>();
    //            paintObject.EraseNearestTriangleSurface(brush, pos);
    //        }
           
    //    }
        private void Reset()
        {
            foreach (var canvas in FindObjectsOfType<InkCanvas>())
                canvas.ResetPaint();
        }
    }



