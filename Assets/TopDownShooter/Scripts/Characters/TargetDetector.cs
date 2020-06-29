using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public abstract class TargetDetector : MonoBehaviour
    {
        public virtual GameObject Target { get; }
    }
}