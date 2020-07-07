using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [System.Serializable]
    public class WeaponInfo
    {
        public WeaponID weaponID = WeaponID.NONE;
        public WeaponStats weaponStats;
        public WeaponController weaponPrefab;
    }

    [System.Serializable]
    public struct WeaponStats
    {
        [Tooltip("Number of bullets fired per second")]
        public int fireRate;
        public int magzineCapacity;
        public int damage;
        public float range;
        public float reloadTime;
        public ProjectileID projectileID;
    }
}