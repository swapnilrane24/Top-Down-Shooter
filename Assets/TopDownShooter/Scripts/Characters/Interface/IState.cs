using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public interface IState
    {
        void Enter(AIControl aiControl);
        void Execute();
        void Exit();
    }
}