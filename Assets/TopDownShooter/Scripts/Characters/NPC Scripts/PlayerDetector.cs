using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TopDownShooter
{
    /// <summary>
    /// This script detect player and its allies
    /// </summary>
    public class PlayerDetector : TargetDetector
    {
        [SerializeField] private float stopChaseRange;
        private GameObject currentTarget;

        public override GameObject Target => currentTarget;
        public float StopChaseRange => stopChaseRange;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IPlayerGroup>() != null)
            {
                if (currentTarget == null)
                {
                    currentTarget = other.gameObject;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IPlayerGroup>() != null)
            {
                if (other.gameObject == currentTarget)
                {
                    currentTarget = null;
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up , stopChaseRange);
        }
#endif
    }
}