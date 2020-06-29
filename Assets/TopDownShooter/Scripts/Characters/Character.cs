using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDownShooter
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        protected Health health;

        
        public Action updateCallbacks;

        protected virtual void Awake()
        {
            health = new Health();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            updateCallbacks?.Invoke();
        }

    }
}
