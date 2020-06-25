using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController instance;

        [SerializeField] private float distance = 20f;
        [SerializeField] private float height = 10f;
        [SerializeField] private float angle = 45f;
        [SerializeField] private float smoothSpeed = 0.5f;

        private GameObject target;
        private Vector3 refVelocity;
        private Vector3 worldPosition, rotatedVector;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        private void Update()
        {
            if (target)
            {
                worldPosition = (Vector3.forward * -distance) + Vector3.up * height;

                rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

                Vector3 flatTargetPosition = target.transform.position;
                flatTargetPosition.y = 0;
                Vector3 finalPos = flatTargetPosition + rotatedVector;

                transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref refVelocity, smoothSpeed);
                transform.LookAt(flatTargetPosition);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            if (target)
            {
                Gizmos.DrawLine(transform.position, target.transform.position);
                Gizmos.DrawSphere(target.transform.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }
    }
}