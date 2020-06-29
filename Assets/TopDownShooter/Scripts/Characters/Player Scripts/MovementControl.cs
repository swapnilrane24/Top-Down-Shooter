using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class MovementControl
    {
        private CharacterController controller;
        private float turnSmoothVelocity;
        private Character characterScript;
        private CharacterMovementStats movementStats;
        private InputControl inputControl;
        private EnemyDetector targetDetector;

        public MovementControl(CharacterController controller, Character characterScript,
            CharacterMovementStats movementStats, InputControl inputControl, EnemyDetector targetDetector)
        {
            this.controller = controller;
            this.characterScript = characterScript;
            this.movementStats = movementStats;
            this.inputControl = inputControl;
            this.targetDetector = targetDetector;
            this.characterScript.updateCallbacks += OnUpdate;
        }

        ~MovementControl()
        {
            characterScript.updateCallbacks -= OnUpdate;
        }

        private void OnUpdate()
        {
            if (inputControl.Direction.magnitude >= 0.05f)
            {
                if (targetDetector.Target == null)
                {
                    float targetAngle = Mathf.Atan2(inputControl.Direction.x, inputControl.Direction.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(characterScript.transform.eulerAngles.y, targetAngle,
                                                        ref turnSmoothVelocity, movementStats.turnSmoothTime);
                    characterScript.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                else
                {
                    Vector3 targetPostition = new Vector3(targetDetector.Target.transform.position.x,
                                           characterScript.transform.position.y,
                                           targetDetector.Target.transform.position.z);
                    characterScript.transform.LookAt(targetPostition);
                }

                controller.Move(inputControl.Direction * movementStats.moveSpeed * Time.deltaTime);
            }
        }

    }
}