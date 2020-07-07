using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private GameObject muzzleFX, hitFX;
        private Vector3 shotDir;
        private IProjectileParent projectileParent;
        private RaycastHit hit;
        private int damage;
        private Vector3 muzzleFxDefaultPos;

        public void SetProjectileParent(IProjectileParent projectileParent, int damage)
        {
            this.projectileParent = projectileParent;
            this.damage = damage;
            muzzleFxDefaultPos = muzzleFX.transform.localPosition;
        }

        public void SetDirection(Vector3 shotDir, float range)
        {
            muzzleFX.transform.localPosition = muzzleFxDefaultPos;
            muzzleFX.transform.parent = transform.parent;
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
                        hit.collider.GetComponent<IEnemyGroup>().Damage(damage);
                    }

                    CancelInvoke();
                    DestroyProjectile();
                }
            }
        }

        private void DestroyProjectile()
        {
            muzzleFX.transform.parent = this.transform;
            muzzleFX.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gameObject.SetActive(false);
            projectileParent.DeactivateProjectile(this);
        }
    }
}