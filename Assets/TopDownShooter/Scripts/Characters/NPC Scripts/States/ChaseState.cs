using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class ChaseState : IState
    {
        private AIControl aiControl;
        private Vector3 direction;
        public void Enter(AIControl aiControl)
        {
            Debug.Log("ChaseState Enter");
            this.aiControl = aiControl;
        }

        public void Execute()
        {
            if (aiControl.Target)
            {
                direction = (aiControl.Target.position - aiControl.ParentObj.position).normalized;
                Rotate();

                if (Vector3.Distance(aiControl.ParentObj.position, aiControl.Target.position) > aiControl.AIInfo.AttackRange)
                {
                    aiControl.ParentObj.Translate(5 * direction * Time.deltaTime, Space.World);
                }
                else
                {
                    aiControl.SwitchState(StateType.ATTACK);
                }
            }
            else
            {
                aiControl.SwitchState(StateType.IDLE);
            }
        }

        public void Exit()
        {
            Debug.Log("ChaseState Enter");
        }

        void Rotate()
        {
            Vector3 targetPostition = new Vector3(aiControl.Target.position.x,
                                                aiControl.ParentObj.position.y,
                                                aiControl.Target.position.z);
            aiControl.AIModel.LookAt(targetPostition);
        }
    }
}