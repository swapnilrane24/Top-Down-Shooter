using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class AICharacter : Character, IEnemyGroup
    {
        private PlayerDetector targetDetector;

        private AIControl aiControl;
        private HealthBarScript healthBarScript;

        public event EventHandler OnKilled;

        public void Damage(int value)
        {
            int currentHealth = health.Damage(value);
            healthBarScript.SetValue(currentHealth / 100f);
            if (currentHealth <= 0)
            {
                OnKilled?.Invoke(this, null);
                Destroy(gameObject);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            aiControl = new DumbAIControl();
        }

        protected override void Start()
        {
            base.Start();
            health.SetMaxHealth(100);
            targetDetector = gameObject.GetComponentInChildren<PlayerDetector>();
            aiControl.Initialize(gameObject, this, targetDetector);
            healthBarScript = gameObject.GetComponentInChildren<HealthBarScript>();
        }
    }
}