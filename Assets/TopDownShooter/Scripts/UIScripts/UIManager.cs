using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class UIManager : MonoBehaviour
    {
        private List<UIBase> uiList;

        private UIBase currentUI;

        private void Start()
        {
            uiList = new List<UIBase>();

            foreach (UIBase item in transform.GetComponentsInChildren<UIBase>(true))
            {
                uiList.Add(item);
            }

            SwitchUI(UIType.MAINMENU);
        }

        public void SwitchUI(UIType uiType)
        {
            currentUI?.Deactivate();

            for (int i = 0; i < uiList.Count; i++)
            {
                if (uiList[i].UIType == uiType)
                {
                    uiList[i]?.Activate();
                    currentUI = uiList[i];
                    break;
                }
            }
        }

    }
}