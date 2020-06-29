using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class PlayerCharacter : Character, IPlayerGroup
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float turnSmoothTime = 0.1f;

        private EnemyDetector targetDetector;
        private WeaponManager weaponManager;

        private InputControl inputControl;
        private MovementControl movementControl;

        protected override void Awake()
        {
            base.Awake();
            inputControl = new InputControl(this);
        }

        protected override void Start()
        {
            base.Start();
            CameraController.instance.SetTarget(this.gameObject);

            CharacterMovementStats stats = new CharacterMovementStats();
            health.SetMaxHealth(100);
            stats.moveSpeed = moveSpeed;
            stats.turnSmoothTime = turnSmoothTime;
            targetDetector = gameObject.GetComponentInChildren<EnemyDetector>();
            weaponManager = gameObject.GetComponentInChildren<WeaponManager>();
            movementControl = new MovementControl(GetComponent<CharacterController>(), this, stats, inputControl, targetDetector);
            weaponManager.InitializeWeapons(inputControl);
        }
    }
}