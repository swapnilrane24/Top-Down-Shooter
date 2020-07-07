using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class MaineMenu : UIBase
    {
        void Awake()
        {
            uiType = UIType.MAINMENU;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnClick(Button btn)
        {
            base.OnClick(btn);

            switch (btn.name)
            {
                case "PlayButton":
                    break;
                case "SettingsButton":
                    break;
                case "CharacterShopButton":
                    break;
                case "WeaponShopButton":
                    break;
            }
        }
    }
}
