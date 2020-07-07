using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class WeaponController : MonoBehaviour, IWeapon, IProjectileParent
    {
        [SerializeField] private GameObject muzzleFlash;

        private WeaponStats weaponStats;
        private ProjectileController projectilePrefab;
        private float timeToFire, currentReloadTime, magzineFillRatio;
        private int currentMagzineFillAmount;
        private List<ProjectileController> deactiveProjectile;
        private Action<bool> coolDownCallback;

        public bool reloading = false;

        public Transform Position { get => muzzleFlash.transform; }
        public bool isReloading => reloading;
        public float MagzineFillRatio => magzineFillRatio;

        public void SetWeaponStats(WeaponStats weaponStats, ProjectileController projectileController)
        {
            this.weaponStats = weaponStats;
            this.projectilePrefab = projectileController;
            currentMagzineFillAmount = weaponStats.magzineCapacity;
            deactiveProjectile = new List<ProjectileController>();

            for (int i = 0; i < 5; i++)
            {
                ProjectileController projectile = Instantiate(projectileController.gameObject, transform).GetComponent<ProjectileController>();
                projectile.SetProjectileParent(this, weaponStats.damage);
                deactiveProjectile.Add(projectile);
                projectile.gameObject.SetActive(false);
            }

            magzineFillRatio = (float)currentMagzineFillAmount / weaponStats.magzineCapacity;
        }

        public virtual void ReleseFire()
        {
            
        }

        public virtual void Fire()
        {
            if (!reloading)
            {
                if (Time.time >= timeToFire)
                {
                    timeToFire = Time.time + 1f / weaponStats.fireRate;
                    CreateProjectile();
                }
                magzineFillRatio = (float)currentMagzineFillAmount / weaponStats.magzineCapacity;
            }
        }

        public virtual void Reload()
        {
            
        }

        public void OnUpdate()
        {
            if (reloading)
            {
                currentReloadTime += Time.deltaTime;
                magzineFillRatio = currentReloadTime / weaponStats.reloadTime;
                if (currentReloadTime >= weaponStats.reloadTime)
                {
                    currentMagzineFillAmount = weaponStats.magzineCapacity;
                    reloading = false;
                    currentReloadTime = 0;
                    coolDownCallback?.Invoke(reloading);
                }
            }
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
                projectile = Instantiate(projectilePrefab.gameObject).GetComponent<ProjectileController>();
                projectile.SetProjectileParent(this, weaponStats.damage);
                projectile.transform.position = muzzleFlash.transform.position;
                projectile.SetDirection(transform.forward, weaponStats.range);
            }

            projectile.transform.rotation = muzzleFlash.transform.rotation;

            currentMagzineFillAmount--;
            if (currentMagzineFillAmount <= 0)
            {
                reloading = true;
                coolDownCallback?.Invoke(reloading);
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

        public void SetWeaponParent(Transform parent, Action<bool> coolDownCallback)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
            this.coolDownCallback = coolDownCallback;
        }
    }
}