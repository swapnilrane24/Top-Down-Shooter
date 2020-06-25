using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class TargetDetector : MonoBehaviour
    {
        private List<GameObject> targetList;

        private GameObject currentTarget;

        public GameObject Target { get { return currentTarget; } }

        private void Awake()
        {
            targetList = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Enemy")
            {
                targetList.Add(other.gameObject);

                if (currentTarget == null)
                {
                    currentTarget = targetList[0];
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Enemy")
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
    }
}
