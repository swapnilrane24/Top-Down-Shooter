using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    public class KeyboardControl : IInput
    {
        private Vector3 direction;
        private PlayerCharacter character;

        public Vector3 Direction { get { return direction; } }

        private Action<bool> gunAttackCallback, meleeAttackCallback;
        private Action gunSwitchCallback;

        public void SetPlayerController(PlayerCharacter character)
        {
            this.character = character;
            this.character.updateCallbacks += OnUpdate;
        }

        public void SetActionCallbacks(Action<bool> gunAttackCallback, Action gunSwitchCallback, Action<bool> meleeAttackCallback)
        {
            this.gunAttackCallback = gunAttackCallback;
            this.gunSwitchCallback = gunSwitchCallback;
            this.meleeAttackCallback = meleeAttackCallback;
        }

        ~KeyboardControl()
        {
            character.updateCallbacks -= OnUpdate;
        }

        public void OnUpdate()
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            
            if (Input.GetMouseButtonDown(0))
            {
                //OnAttack?.Invoke(this, new OnAttackEvent { attack = true});
                gunAttackCallback?.Invoke(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                //OnAttack?.Invoke(this, new OnAttackEvent { attack = false });
                gunAttackCallback?.Invoke(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //OnSwitch?.Invoke(this, new EventArgs());
                gunSwitchCallback?.Invoke();
            }
        }
    }
}