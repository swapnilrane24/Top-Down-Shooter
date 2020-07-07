using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class MovementControl
    {
        private CharacterController controller;
        private float turnSmoothVelocity;
        private PlayerCharacter characterScript;
        private CharacterMovementStats movementStats;
        private IInput inputControl;
        private EnemyDetector targetDetector;

        public MovementControl(CharacterController controller, PlayerCharacter characterScript,
            CharacterMovementStats movementStats, IInput inputControl, EnemyDetector targetDetector)
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
                    float angle = Mathf.SmoothDampAngle(characterScript.PlayerModel.eulerAngles.y, targetAngle,
                                                        ref turnSmoothVelocity, movementStats.turnSmoothTime);
                    characterScript.PlayerModel.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                else
                {
                    Vector3 targetPostition = new Vector3(targetDetector.Target.transform.position.x,
                                           characterScript.transform.position.y,
                                           targetDetector.Target.transform.position.z);
                    characterScript.PlayerModel.LookAt(targetPostition);
                }

                controller.Move(inputControl.Direction * movementStats.moveSpeed * Time.deltaTime);
            }
        }

    }
}