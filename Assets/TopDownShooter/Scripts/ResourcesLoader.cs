using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class ResourcesLoader : MonoBehaviour
    {
        public static ResourcesLoader instance;

        [SerializeField] private WeaponListScriptable weaponList;
        [SerializeField] private ProjectileListScriptable projectileList;

        public WeaponListScriptable WeaponScriptableList { get { return weaponList; } }
        public ProjectileListScriptable ProjectileList { get { return projectileList; } }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
}
