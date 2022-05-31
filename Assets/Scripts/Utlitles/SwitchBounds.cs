using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace FarmGame
{
    public class SwitchBounds : MonoBehaviour
    {
        //TO 切换场景后调用
        private void Start()
        {
            SwitchConfinerShape();
        }

        private void SwitchConfinerShape()
        {
            PolygonCollider2D confinerShaper =
                GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();

            CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
            confiner.m_BoundingShape2D = confinerShaper;
            //Call this if the bounding shape's points change at runtime
            confiner.InvalidatePathCache();
        }
    }
}
