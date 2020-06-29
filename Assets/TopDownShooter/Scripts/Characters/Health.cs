using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Health
    {
        private int health;
        private int maxHealth;
        public void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
        }

        public int Damage(int damValue)
        {
            return health -= damValue;
        }

        public void Heal(int healValue)
        {
            health += healValue;
        }

        public void Revive()
        {
            health = maxHealth;
        }


    }
}