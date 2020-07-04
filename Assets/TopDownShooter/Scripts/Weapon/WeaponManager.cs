using UnityEngine;

namespace TopDownShooter
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private GameObject weaponHolder;

        private WeaponFactory weaponFactory;
        private PlayerCharacter playerCharacter;
        private WeaponBarScript weaponBarScript;
        private IWeapon primaryWeapon, secondaryWeapon, activeWeapon;
        private bool attack = false;

        public void InitializeWeapons(PlayerCharacter playerCharacter, WeaponBarScript weaponBarScript)
        {
            this.playerCharacter = playerCharacter;
            this.weaponBarScript = weaponBarScript;
            weaponFactory = new WeaponFactory();
            primaryWeapon = weaponFactory.SpawnWeapon(WeaponID.PISTON);
            primaryWeapon.SetWeaponParent(weaponHolder.transform, GunReloading);

            secondaryWeapon = weaponFactory.SpawnWeapon(WeaponID.MACHINEGUN);
            secondaryWeapon.SetWeaponParent(weaponHolder.transform, GunReloading);

            secondaryWeapon.ActivateWeapon();
            activeWeapon = secondaryWeapon;
        }

        private void Update()
        {
            if (attack && !activeWeapon.isReloading)
            {
                activeWeapon.Fire();
            }
            weaponBarScript.SetValue(activeWeapon.MagzineFillRatio);
            activeWeapon.OnUpdate();
        }

        public void Attack(bool value)
        {
            attack = value;
            if (attack == false)
            {
                activeWeapon.ReleseFire();
                playerCharacter.PlayerState = PlayerState.NORMAL;
            }
        }

        public void SwitchWeapon()
        {
            activeWeapon.DeactivateWeapon();
            activeWeapon = activeWeapon == secondaryWeapon ? primaryWeapon : secondaryWeapon;
            activeWeapon.ActivateWeapon();
            weaponBarScript.SetValue(activeWeapon.MagzineFillRatio);
        }

        private void GunReloading(bool value)
        {
            playerCharacter.PlayerState = value ? PlayerState.RELOAD : PlayerState.NORMAL;
        }
    }
}
