using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public abstract class UIBase : MonoBehaviour
    {
        protected UIType uiType;

        private List<Button> buttonsList;

        public UIType UIType => uiType;

        protected virtual void Start()
        {
            foreach (Button item in FindObjectsOfType<Button>())
            {
                item.onClick.AddListener(() => OnClick(item));
            }
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnClick(Button btn)
        {
            //Debug.Log(btn.name + " Clicked");
        }
    }
}