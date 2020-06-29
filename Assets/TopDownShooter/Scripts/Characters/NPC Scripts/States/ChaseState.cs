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
                direction = (aiControl.Target.position - aiControl.ParentObj.transform.position).normalized;
                Rotate();

                aiControl.ParentObj.transform.Translate(5 * direction * Time.deltaTime, Space.World);

                if (Vector3.Distance(aiControl.ParentObj.transform.position, aiControl.Target.position) > aiControl.StopChaseRange)
                {
                    aiControl.SwitchState(StateType.IDLE);
                }
            }
        }

        public void Exit()
        {
            Debug.Log("ChaseState Enter");
        }

        void Rotate()
        {
            Vector3 targetPostition = new Vector3(aiControl.Target.position.x,
                                                aiControl.ParentObj.transform.position.y,
                                                aiControl.Target.position.z);
            aiControl.ParentObj.transform.LookAt(targetPostition);
        }
    }
}