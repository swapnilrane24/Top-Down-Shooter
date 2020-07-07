using System;
using UnityEngine;
namespace TopDownShooter
{
    public interface IInput
    {
        Vector3 Direction { get; }
        void SetActionCallbacks(Action<bool> gunAttackCallback, Action gunSwitchCallback, Action<bool> meleeAttackCallback);
        void SetPlayerController(PlayerCharacter character);

        void OnUpdate();
    }
}