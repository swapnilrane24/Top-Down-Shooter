using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    public interface IEnemyGroup
    {
        event EventHandler OnKilled;
        void Damage(int value);
    }
}