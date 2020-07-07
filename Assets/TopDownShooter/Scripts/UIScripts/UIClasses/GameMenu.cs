using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TopDownShooter
{
    public class GameMenu : UIBase
    {
        public static GameMenu instance;

        [SerializeField] private FixedJoystick fixedJoystick;

        public event EventHandler OnSwitch;
        public event EventHandler<OnAttackEvent> OnAttack;
        public event EventHandler<OnDirectionEvent> OnDirection;
        public event EventHandler OnHeal;

        private bool joystickInUse = false;
        private Vector3 direction;

        // Start is called before the first frame update
        void Awake()
        {
            uiType = UIType.GAMEMENU;
            if (instance == null)
            {
                instance = this;
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public void OnJoytickDonw(bool value)
        {
            joystickInUse = value;
            if (!value)
            {
                direction = Vector3.zero;
                OnDirection?.Invoke(this, new OnDirectionEvent { direction = direction });
            }
        }

        private void Update()
        {
            if (joystickInUse)
            {
                direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
                OnDirection?.Invoke(this, new OnDirectionEvent { direction = direction });
            }
        }

        protected override void OnClick(Button btn)
        {
            base.OnClick(btn);

            switch (btn.name)
            {
                case "HealButton":
                    break;
                case "SwitchGunButton":
                    OnSwitch?.Invoke(this, new EventArgs());
                    break;
                case "ExplosionShopButton":
                    break;
                case "PauseButton":
                    break;
            }
        }

        public void OnFireDown()
        {
            OnAttack?.Invoke(this, new OnAttackEvent { attack = true });
        }

        public void OnFireUp()
        {
            OnAttack?.Invoke(this, new OnAttackEvent { attack = false });
        }
    }
}