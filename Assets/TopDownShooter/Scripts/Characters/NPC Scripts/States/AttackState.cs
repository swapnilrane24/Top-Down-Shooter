using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class AttackState : IState
    {
        private AIControl aiControl;

        public void Enter(AIControl aiControl)
        {
            this.aiControl = aiControl;
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
