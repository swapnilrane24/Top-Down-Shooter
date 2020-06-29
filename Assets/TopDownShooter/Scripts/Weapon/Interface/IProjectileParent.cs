using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public interface IProjectileParent
    {
        Transform Position { get; }
        void DeactivateProjectile(ProjectileController projectile);
    }
}