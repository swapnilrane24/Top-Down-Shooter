using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TopDownShooter
{
    public class AICharacter : Character, IEnemyGroup
    {
        [SerializeField] private AIInfo aIInfo;

        private PlayerDetector targetDetector;

        private AIControl aiControl;
        private HealthBarScript healthBarScript;
        private Transform enemyModel;

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
            enemyModel = transform.Find("EnemyModel");
            targetDetector = gameObject.GetComponentInChildren<PlayerDetector>();
            healthBarScript = gameObject.GetComponentInChildren<HealthBarScript>();

            aiControl.Initialize(enemyModel, this, targetDetector, aIInfo);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position - Vector3.up * 0.98f, transform.up, aIInfo.AttackRange);
        }
#endif

    }
}