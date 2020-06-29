using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class AIControl
    {
        protected IState currentState;
        protected IState idleState, chaseState, patrolState, attackState;
        protected EnemyType enemyType = EnemyType.DUMB;

        private float stopChaseRange;
        private Transform target;
        private Character character;
        private GameObject parentObj;
        private PlayerDetector playerDetector;

        public float StopChaseRange => stopChaseRange;
        public EnemyType EnemyType { get { return enemyType; } }
        public Transform Target { get { return target; } }
        public GameObject ParentObj { get { return parentObj; } }

        public void Initialize(GameObject parent, Character character, PlayerDetector targetDetector)
        {
            this.character = character;
            parentObj = parent;
            this.playerDetector = targetDetector;
            this.character.updateCallbacks += OnUpdate;
            stopChaseRange = this.playerDetector.StopChaseRange;
            SetStates();
        }

        ~AIControl()
        {
            character.updateCallbacks -= OnUpdate;
        }

        protected virtual void SetStates() { }

        public virtual void OnUpdate()
        {
            currentState.Execute();

            if (playerDetector.Target != null)
            {
                if (currentState != chaseState)
                {
                    SwitchState(StateType.CHASE);
                }
            }
        }

        public void SwitchState(StateType stateType)
        {
            currentState.Exit();
            switch (stateType)
            {
                case StateType.IDLE:
                    target = null;
                    currentState = idleState;
                    break;
                case StateType.CHASE:
                    target = playerDetector.Target.transform;
                    currentState = chaseState;
                    break;
                case StateType.PATROL:
                    currentState = patrolState;
                    break;
                case StateType.ATTACK:
                    currentState = attackState;
                    break;
            }
            currentState.Enter(this);
        }
    }
}