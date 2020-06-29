using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [CreateAssetMenu(fileName = "WeaponList", menuName = "ScriptableObjects/Create WeaponList", order = 1)]
    public class WeaponListScriptable : ScriptableObject
    {
        public List<WeaponInfo> weaponList;
    }
}