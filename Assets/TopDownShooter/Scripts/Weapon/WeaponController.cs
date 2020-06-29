using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class WeaponController : MonoBehaviour, IWeapon, IProjectileParent
    {
        [SerializeField] private GameObject muzzleFlash;

        private WeaponStats weaponStats;
        private ProjectileController projectileController;
        private float currentTime;

        private List<ProjectileController> deactiveProjectile;

        public Transform Position { get => muzzleFlash.transform; }

        public void SetWeaponStats(WeaponStats weaponStats, ProjectileController projectileController)
        {
            this.weaponStats = weaponStats;
            this.projectileController = projectileController;
            deactiveProjectile = new List<ProjectileController>();

            for (int i = 0; i < 5; i++)
            {
                ProjectileController projectile = Instantiate(projectileController.gameObject, transform).GetComponent<ProjectileController>();
                projectile.SetProjectileParent(this);
                deactiveProjectile.Add(projectile);
                projectile.gameObject.SetActive(false);
            }
        }

        public virtual void ReleseFire()
        {
            currentTime = 0;
        }

        public virtual void Fire()
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = weaponStats.fireRate;
                CreateProjectile();
            }
        }

        public virtual void Reload()
        {
            
        }

        protected virtual void CreateProjectile()
        {
            ProjectileController projectile = null;
            if (deactiveProjectile.Count > 0)
            {
                projectile = deactiveProjectile[0];
                projectile.transform.position = muzzleFlash.transform.position;
                projectile.SetDirection(transform.forward, weaponStats.range);
                deactiveProjectile.Remove(projectile);
                projectile.transform.parent = null;
                projectile.gameObject.SetActive(true);
            }
            else
            {
                projectile = Instantiate(projectileController.gameObject).GetComponent<ProjectileController>();
                projectile.SetProjectileParent(this);
                projectile.transform.position = muzzleFlash.transform.position;
                projectile.SetDirection(transform.forward, weaponStats.range);
            }
        }

        public virtual void ActivateWeapon()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeactivateWeapon()
        {
            gameObject.SetActive(false);
        }

        public virtual void DeactivateProjectile(ProjectileController projectile)
        {
            projectile.transform.SetParent(gameObject.transform);
            deactiveProjectile.Add(projectile);
        }

        public void SetWeaponParent(Transform parent)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(muzzleFlash.transform.position, transform.parent.forward * weaponStats.range);
        //}
    }
}