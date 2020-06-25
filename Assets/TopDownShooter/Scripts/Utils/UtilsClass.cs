using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public abstract class UtilsClass
    {
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return worldPos;
        }
    }
}