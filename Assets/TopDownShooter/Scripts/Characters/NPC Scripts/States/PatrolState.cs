using UnityEngine;

namespace TopDownShooter
{
    public class PatrolState : IState
    {
        private AIControl aiControl;
        private Vector3 targetPos;
        private Vector3 direction;

        public void Enter(AIControl aiControl)
        {
            Debug.Log("PatrolState Enter");
            this.aiControl = aiControl;

            targetPos = RandomPos();
            Vector3 difference = aiControl.ParentObj.transform.position - targetPos;
            direction = new Vector3(difference.x, 0f, difference.z).normalized;
        }

        public void Execute()
        {
            aiControl.ParentObj.transform.position = Vector3.MoveTowards(aiControl.ParentObj.transform.position,
                targetPos, 3.0f * Time.deltaTime);

            if (Vector3.Distance(aiControl.ParentObj.transform.position, targetPos) <= 0.5f)
            {
                aiControl.SwitchState(StateType.IDLE);
            }
        }

        public void Exit()
        {
            Debug.Log("PatrolState Exit");
        }

        private Vector3 RandomPos()
        {
            Vector3 targetPostition = Vector3.zero;
            targetPostition.x = aiControl.ParentObj.transform.position.x + Random.Range(-15, 15);
            targetPostition.z = aiControl.ParentObj.transform.position.z + Random.Range(-15, 15);
            targetPostition.y = aiControl.ParentObj.transform.position.y;
            aiControl.ParentObj.transform.LookAt(targetPostition);
            return targetPostition;
        }
    }
}
