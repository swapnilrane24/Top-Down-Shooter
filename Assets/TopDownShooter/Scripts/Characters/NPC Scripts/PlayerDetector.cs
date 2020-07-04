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
        private AIControl aiControl;

        public override GameObject Target { get { return currentTarget; } }
        public float StopChaseRange => stopChaseRange;

        public void SetAIController(AIControl aiControl)
        {
            this.aiControl = aiControl;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IPlayerGroup>() != null)
            {
                if (currentTarget == null)
                {
                    currentTarget = other.gameObject;
                    aiControl.Target = currentTarget.transform;
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
                    aiControl.Target = null;
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position - Vector3.up * 0.98f, transform.up , stopChaseRange);
        }
#endif
    }
}