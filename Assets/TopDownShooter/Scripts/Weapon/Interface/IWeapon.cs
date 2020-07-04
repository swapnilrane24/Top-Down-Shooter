using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public interface IWeapon
    {
        bool isReloading { get; }
        float MagzineFillRatio { get; }
        void SetWeaponParent(Transform parent, Action<bool> coolDownCallback);
        void ActivateWeapon();
        void DeactivateWeapon();
        void Fire();
        void ReleseFire();
        void Reload();

        void OnUpdate();
    }
}