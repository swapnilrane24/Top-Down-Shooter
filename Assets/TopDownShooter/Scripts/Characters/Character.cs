using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private CharacterType characterType = CharacterType.NPC;
        [SerializeField] private TargetDetector targetDetector;


        private InputControl inputControl;
        private MovementControl movementControl;

        public Action fixedUpdateCallBack;
        public Action updateCallbacks;

        private void Awake()
        {
            CharacterMovementStats stats = new CharacterMovementStats();
            stats.moveSpeed = moveSpeed;
            stats.turnSmoothTime = turnSmoothTime;
            inputControl = new InputControl(this);
            movementControl = new MovementControl(GetComponent<CharacterController>(), this, stats, inputControl, targetDetector);
        }

        private void Start()
        {
            CameraController.instance.SetTarget(this.gameObject);
        }

        private void Update()
        {
            updateCallbacks?.Invoke();
        }


    }

    public enum CharacterType
    {
        PLAYER,
        NPC
    }

    public class CharacterMovementStats
    {
        public float moveSpeed;
        public float turnSmoothTime;
    }
}
