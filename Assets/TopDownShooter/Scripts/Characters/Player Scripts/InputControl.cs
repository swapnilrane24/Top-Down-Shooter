using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    public class InputControl
    {
        private Vector3 direction;
        private PlayerCharacter character;

        public Vector3 Direction { get { return direction; } }

        public event EventHandler OnSwitch;
        public event EventHandler<OnAttackEvent> OnAttack;

        private Action<bool> gunAttackCallback, meleeAttackCallback;
        private Action gunSwitchCallback;

        public class OnAttackEvent : EventArgs
        {
            public bool attack;
        }

        public InputControl(PlayerCharacter character)
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

        ~InputControl()
        {
            character.updateCallbacks -= OnUpdate;
        }

        private void OnUpdate()
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