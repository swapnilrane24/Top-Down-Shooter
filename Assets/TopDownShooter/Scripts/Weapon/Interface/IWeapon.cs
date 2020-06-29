using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public interface IWeapon
    {
        void SetWeaponParent(Transform parent);
        void ActivateWeapon();
        void DeactivateWeapon();
        void Fire();
        void ReleseFire();
        void Reload();
    }
}