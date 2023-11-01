using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Animations.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class FireState : FiniteStateBase
    {
        private readonly IWeaponView _weaponView;
        private readonly IWeaponAnimation _weaponAnimation;
        private readonly IWeapon _weapon;
        private readonly TargetTrackerSystem _targetTrackerSystem;

        public FireState(
            IWeaponView weaponView,
            IWeaponAnimation weaponAnimation,
            IWeapon weapon,
            TargetTrackerSystem targetTrackerSystem
        )
        {
            _weaponView = weaponView;
            _weaponAnimation = weaponAnimation;
            _weapon = weapon;
            _targetTrackerSystem = targetTrackerSystem;
        }

        protected override void OnExit()
        {
//            _weapon
            _weaponAnimation.Fire();
        }

        protected override void OnEnter()
        {
        }
    }
}