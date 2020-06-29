using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class LookAtCam : MonoBehaviour
    {
        private Camera targetCam;
        private Quaternion finalRotation;

        private void Awake()
        {
            targetCam = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(targetCam.transform.position);
        }
    }
}