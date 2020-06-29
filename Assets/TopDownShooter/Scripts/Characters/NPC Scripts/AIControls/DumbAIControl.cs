using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class DumbAIControl : AIControl
    {
        protected override void SetStates()
        {
            base.SetStates();

            idleState = new IdleState();
            patrolState = new PatrolState();
            chaseState = new ChaseState();
            attackState = new AttackState();

            currentState = idleState;
            currentState.Enter(this);
        }
    }
}