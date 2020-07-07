using System;
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
        private HealthBarScript healthBarScript;
        private WeaponBarScript weaponBarScript;
        private IInput inputControl;
        private MovementControl movementControl;
        private Transform playerModel;
        private PlayerState playerState = PlayerState.NORMAL;

        public Transform PlayerModel { get { return playerModel; } }

        public PlayerState PlayerState { get => playerState; set => playerState = value; }

        public void TakeDamage(int value)
        {
            int currentHealth = health.Damage(value);
            healthBarScript.SetValue(currentHealth / 100f);
            if (currentHealth <= 0)
            {
                playerState = PlayerState.DIE;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            inputControl = new TouchControl();

//#if UNITY_EDITOR
//            inputControl = new KeyboardControl();
//#elif UNITY_ANDROID || UNITY_IOS
//            inputControl = new TouchControl();
//#endif
            inputControl.SetActionCallbacks(OnGunAttack, OnGunSwitch, OnMeleeAttack);
            healthBarScript = gameObject.GetComponentInChildren<HealthBarScript>();
            weaponBarScript = gameObject.GetComponentInChildren<WeaponBarScript>();
            playerModel = transform.Find("Model");
            playerState = PlayerState.NORMAL;
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
            weaponManager.InitializeWeapons(this, weaponBarScript);
        }

        protected override void Update()
        {
            if (playerState == PlayerState.DIE)
            {
                return;
            }

            base.Update();
        }

        private void OnGunAttack(bool value)
        {
            if (playerState != PlayerState.RELOAD || playerState != PlayerState.DIE)
            {
                if (playerState != PlayerState.ATTACK)
                {
                    playerState = PlayerState.ATTACK;
                }
                weaponManager.Attack(value);
            }
        }

        private void OnGunSwitch()
        {
            if (playerState != PlayerState.DIE)
                weaponManager.SwitchWeapon();
        }

        private void OnMeleeAttack(bool value)
        {
            if (playerState != PlayerState.DIE)
            {
                
            }
        }
    }

    public enum PlayerState
    {
        NORMAL,
        ATTACK,
        RELOAD,
        DIE
    }
}