using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class TouchControl : IInput
    {
        private Vector3 direction;
        private PlayerCharacter character;
        public Vector3 Direction => direction;

        private Action<bool> gunAttackCallback, meleeAttackCallback;
        private Action gunSwitchCallback;

        public void SetActionCallbacks(Action<bool> gunAttackCallback, Action gunSwitchCallback, Action<bool> meleeAttackCallback)
        {
            this.gunAttackCallback = gunAttackCallback;
            this.gunSwitchCallback = gunSwitchCallback;
            this.meleeAttackCallback = meleeAttackCallback;
            GameMenu.instance.OnAttack += Attack;
            GameMenu.instance.OnSwitch += Switch;
            GameMenu.instance.OnDirection += OnDirection;
        }

        public void SetPlayerController(PlayerCharacter character)
        {
            this.character = character;
            //this.character.updateCallbacks += OnUpdate;
        }

        public void OnUpdate()
        {
            
        }

        void Attack(object sender, OnAttackEvent e)
        {
            gunAttackCallback?.Invoke(e.attack);
        }

        void OnDirection(object sender, OnDirectionEvent e)
        {
            direction = e.direction;
        }

        void Switch(object sender, EventArgs e)
        {
            gunSwitchCallback?.Invoke();
        }

        ~TouchControl()
        {
            GameMenu.instance.OnAttack -= Attack;
            GameMenu.instance.OnSwitch -= Switch;
            GameMenu.instance.OnDirection -= OnDirection;
            //character.updateCallbacks -= OnUpdate;
        }
    }
}