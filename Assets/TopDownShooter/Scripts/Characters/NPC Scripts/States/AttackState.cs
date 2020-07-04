using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class AttackState : IState
    {
        private AIControl aiControl;
        private float attackRate;
        private float currentAttackTime;

        public void Enter(AIControl aiControl)
        {
            Debug.Log("AttackState Enter");
            this.aiControl = aiControl;
            attackRate = aiControl.AIInfo.AttackRate;
            currentAttackTime = 0;
        }

        public void Execute()
        {
            if (aiControl.Target)
            {
                if (Vector3.Distance(aiControl.ParentObj.position, aiControl.Target.position) <= aiControl.AIInfo.AttackRange)
                {
                    currentAttackTime -= Time.deltaTime;
                    if (currentAttackTime <= 0)
                    {
                        AttackMethod();
                        currentAttackTime = attackRate;
                    }
                }
                else
                {
                    aiControl.SwitchState(StateType.CHASE);
                }
            }
        }

        void AttackMethod()
        {
            Debug.Log("Damage");
            if (aiControl.Target.GetComponent<IPlayerGroup>() != null)
            {
                aiControl.Target.GetComponent<IPlayerGroup>().TakeDamage(aiControl.AIInfo.DamageAmount);
            }
        }

        public void Exit()
        {
            Debug.Log("AttackState Exit");
        }
    }
}
