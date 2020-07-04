using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class IdleState : IState
    {
        private AIControl aiControl;

        private float timer = 0;

        public void Enter(AIControl aiControl)
        {
            this.aiControl = aiControl;
            Debug.Log("IdleState Enter");
            timer = 0;
        }

        public void Execute()
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                aiControl.SwitchState(StateType.PATROL);
            }

            if (aiControl.Target != null)
            {
                aiControl.SwitchState(StateType.CHASE);
            }
        }

        public void Exit()
        {
            Debug.Log("IdleState Exit");
        }
    }
}