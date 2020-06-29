using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    /// <summary>
    /// This script detect enemy and its allies
    /// </summary>
    public class EnemyDetector : TargetDetector, INonDamagable
    {
        private List<GameObject> targetList;

        private GameObject currentTarget;

        public override GameObject Target => currentTarget;

        private void Awake()
        {
            targetList = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IEnemyGroup>() != null)
            {
                targetList.Add(other.gameObject);

                if (currentTarget == null)
                {
                    currentTarget = targetList[0];
                    currentTarget.GetComponent<IEnemyGroup>().OnKilled += TargetKilled;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IEnemyGroup>() != null)
            {
                targetList.Remove(other.gameObject);
                if (other.gameObject == currentTarget)
                {
                    currentTarget = null;

                    if (targetList.Count > 0)
                    {
                        currentTarget = targetList[0];
                    }
                }
            }
        }

        void TargetKilled(object sender, EventArgs e)
        {
            currentTarget.GetComponent<IEnemyGroup>().OnKilled -= TargetKilled;
            targetList.Remove(currentTarget);
            currentTarget = null;

            if (targetList.Count > 0)
            {
                currentTarget = targetList[0];
            }
        }
    }
}
