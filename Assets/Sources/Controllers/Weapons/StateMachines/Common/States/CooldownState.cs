using Sources.Infrastructure.FiniteStateMachines.States;
using UnityEngine;

namespace Sources.Controllers.Weapons.StateMachines.Common.States
{
    public class CooldownState : FiniteStateBase
    {
        protected override void OnEnter()
        {
            Debug.Log(" CooldownState");
        }
    }
}