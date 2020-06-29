using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    public class InputControl
    {
        private Vector3 direction;
        private Character character;

        public Vector3 Direction { get { return direction; } }

        public event EventHandler OnSwitch;
        public event EventHandler<OnAttackEvent> OnAttack;

        public class OnAttackEvent : EventArgs
        {
            public bool attack;
        }

        public InputControl(Character character)
        {
            this.character = character;
            this.character.updateCallbacks += OnUpdate;
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
                OnAttack?.Invoke(this, new OnAttackEvent { attack = true});
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnAttack?.Invoke(this, new OnAttackEvent { attack = false });
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnSwitch?.Invoke(this, new EventArgs());
            }
        }

    }
}