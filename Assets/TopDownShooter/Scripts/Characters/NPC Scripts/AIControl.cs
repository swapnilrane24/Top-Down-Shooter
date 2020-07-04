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

        private Transform target;
        private AICharacter character;
        private PlayerDetector playerDetector;
        private Transform modelObj, parentObj;
        private AIInfo aIInfo;

        public AIInfo AIInfo => aIInfo;
        public EnemyType EnemyType { get { return enemyType; } }
        public Transform Target { get { return target; } set { target = value; } }
        public Transform AIModel { get { return modelObj; } }
        public Transform ParentObj { get { return parentObj; } }

        public void Initialize(Transform modelObj, AICharacter character, PlayerDetector targetDetector, AIInfo aIInfo)
        {
            this.aIInfo = aIInfo;
            this.aIInfo.StopChaseRange = targetDetector.StopChaseRange;
            this.character = character;
            parentObj = character.gameObject.transform;
            this.modelObj = modelObj;
            playerDetector = targetDetector;
            playerDetector.SetAIController(this);
            this.character.updateCallbacks += OnUpdate;
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
        }

        public void SwitchState(StateType stateType)
        {
            currentState.Exit();
            switch (stateType)
            {
                case StateType.IDLE:
                    currentState = idleState;
                    break;
                case StateType.CHASE:
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