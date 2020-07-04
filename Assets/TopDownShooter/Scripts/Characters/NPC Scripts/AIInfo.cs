using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [System.Serializable]
    public class AIInfo
    {
        [SerializeField] private float attackRange, attackRate;
        [SerializeField] private int damageAmount;

        private float stopChaseRange;

        public float AttackRange { get => attackRange; }
        public float AttackRate { get => attackRate; }
        public int DamageAmount { get => damageAmount; }
        public float StopChaseRange { get => stopChaseRange; set => stopChaseRange = value; }
    }
}