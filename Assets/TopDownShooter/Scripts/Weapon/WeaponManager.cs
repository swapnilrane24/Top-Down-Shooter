using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private GameObject weaponHolder;

        private IWeapon primaryWeapon, secondaryWeapon;
        private WeaponFactory weaponFactory;

        private IWeapon activeWeapon;
        private InputControl inputControl;
        private bool attack = false;

        public void InitializeWeapons(InputControl inputControl)
        {
            this.inputControl = inputControl;
            weaponFactory = new WeaponFactory();
            primaryWeapon = weaponFactory.SpawnWeapon(WeaponID.PISTON);
            primaryWeapon.SetWeaponParent(weaponHolder.transform);

            secondaryWeapon = weaponFactory.SpawnWeapon(WeaponID.MACHINEGUN);
            secondaryWeapon.SetWeaponParent(weaponHolder.transform);

            secondaryWeapon.ActivateWeapon();
            activeWeapon = secondaryWeapon;

            inputControl.OnSwitch += SwitchWeapon;
            inputControl.OnAttack += Attack;
        }

        private void OnDisable()
        {
            inputControl.OnSwitch -= SwitchWeapon;
            inputControl.OnAttack -= Attack;
        }

        private void Update()
        {
            if (attack)
            {
                activeWeapon.Fire();
            }
        }

        private void Attack(object sender, InputControl.OnAttackEvent e)
        {
            attack = e.attack;
            if (attack == false)
            {
                activeWeapon.ReleseFire();
            }
        }

        public void SwitchWeapon(object sender, EventArgs e)
        {
            activeWeapon.DeactivateWeapon();
            activeWeapon = activeWeapon == secondaryWeapon ? primaryWeapon : secondaryWeapon;
            activeWeapon.ActivateWeapon();
        }

    }
}
