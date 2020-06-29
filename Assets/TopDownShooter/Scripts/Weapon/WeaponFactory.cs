using UnityEngine;

namespace TopDownShooter
{
    public class WeaponFactory
    {
        public WeaponController SpawnWeapon(WeaponID weaponID)
        {
            WeaponController weapon = null;
            foreach (WeaponInfo weaponinfo in ResourcesLoader.instance.WeaponScriptableList.weaponList)
            {
                if (weaponinfo.weaponID == weaponID)
                {
                    GameObject weaponObj = Object.Instantiate(weaponinfo.weaponPrefab.gameObject);
                    weapon = weaponObj.GetComponent<WeaponController>();
                    weapon.SetWeaponStats(weaponinfo.weaponStats, GetProjectile(weaponinfo.weaponStats.projectileID));
                    weaponObj.SetActive(false);
                    break;
                }
            }

            return weapon;
        }

        private ProjectileController GetProjectile(ProjectileID projectileID)
        {
            ProjectileController projectile = null;

            foreach (ProjectileInfo projectileObj in ResourcesLoader.instance.ProjectileList.projectiles)
            {
                if (projectileObj.projectileID == projectileID)
                {
                    projectile = projectileObj.projectile;
                    break;
                }
            }
            return projectile;
        }

    }
}