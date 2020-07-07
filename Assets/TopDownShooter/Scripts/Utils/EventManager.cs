using System;
using UnityEngine;

namespace TopDownShooter
{
    public class OnAttackEvent : EventArgs
    {
        public bool attack;
    }

    public class OnDirectionEvent : EventArgs
    {
        public Vector3 direction;
    }
}