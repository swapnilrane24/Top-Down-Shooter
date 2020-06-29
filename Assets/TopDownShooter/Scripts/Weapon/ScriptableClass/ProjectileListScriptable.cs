using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [CreateAssetMenu(fileName = "ProjectileList", menuName = "ScriptableObjects/Create ProjectileList", order = 3)]
    public class ProjectileListScriptable : ScriptableObject
    {
        public List<ProjectileInfo> projectiles;
    }

    [System.Serializable]
    public class ProjectileInfo
    {
        public ProjectileID projectileID;
        public ProjectileController projectile;
    }
}