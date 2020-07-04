using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class LookAtCam : MonoBehaviour
    {
        private Camera targetCam;

        private void Awake()
        {
            targetCam = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - targetCam.transform.position);
        }
    }
}