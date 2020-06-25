using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class InputControl
    {
        private Vector3 direction;
        private Character character;

        public Vector3 Direction { get { return direction; } }

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
        }

    }
}