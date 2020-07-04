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
            Vector3 difference = aiControl.ParentObj.position - targetPos;
            direction = new Vector3(difference.x, 0f, difference.z).normalized;
        }

        public void Execute()
        {
            //need to move the aimodel parent
            aiControl.ParentObj.position = Vector3.MoveTowards(aiControl.ParentObj.position, targetPos, 3.0f * Time.deltaTime);

            if (Vector3.Distance(aiControl.ParentObj.position, targetPos) <= 0.5f)
            {
                aiControl.SwitchState(StateType.IDLE);
            }

            if (aiControl.Target != null)
            {
                aiControl.SwitchState(StateType.CHASE);
            }
        }

        public void Exit()
        {
            Debug.Log("PatrolState Exit");
        }

        private Vector3 RandomPos()
        {
            Vector3 targetPostition = Vector3.zero;
            targetPostition.x = aiControl.ParentObj.position.x + Random.Range(-15, 15);
            targetPostition.z = aiControl.ParentObj.position.z + Random.Range(-15, 15);
            targetPostition.y = aiControl.ParentObj.position.y;
            //rotate ai model
            aiControl.AIModel.LookAt(targetPostition);
            return targetPostition;
        }
    }
}
