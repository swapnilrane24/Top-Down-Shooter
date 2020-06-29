using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class ProjectileController : MonoBehaviour
    {
        private Vector3 shotDir;
        private IProjectileParent projectileParent;
        private RaycastHit hit;

        public void SetProjectileParent(IProjectileParent projectileParent)
        {
            this.projectileParent = projectileParent;
        }

        public void SetDirection(Vector3 shotDir, float range)
        {
            this.shotDir = shotDir;
            Invoke("DestroyProjectile", range / 50);
        }

        private void Update()
        {
            transform.position += shotDir * 35 * Time.deltaTime;
            Collided();
        }

        private void Collided()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<INonDamagable>() != null)
                    {
                        return;
                    }

                    if (hit.collider.GetComponent<IEnemyGroup>() != null)
                    {
                        Debug.Log("Enemy Detected");
                        hit.collider.GetComponent<IEnemyGroup>().Damage(15);
                    }

                    CancelInvoke();
                    DestroyProjectile();
                }
            }
        }

        private void DestroyProjectile()
        {
            gameObject.SetActive(false);
            projectileParent.DeactivateProjectile(this);
        }
    }
}