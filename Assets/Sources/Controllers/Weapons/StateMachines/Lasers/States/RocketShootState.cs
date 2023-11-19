using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Weapons.Rockets;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class RocketShootState : FiniteStateBase
    {
        private readonly IRocketWeapon _rocketWeapon;
        private readonly IWeaponView _view;
        private readonly ITargetProvider _targetProvider;

        public RocketShootState(IRocketWeapon rocketWeapon, IWeaponView view, ITargetProvider targetProvider)
        {
            _rocketWeapon = rocketWeapon ?? throw new ArgumentNullException(nameof(rocketWeapon));
            _view = view;
            _targetProvider = targetProvider;
        }

        protected override void OnEnter()
        {
            _rocketWeapon.SetDestination(_targetProvider.GetTarget().Position);
            Shoot();
        }

        private async UniTask Shoot()
        {
            for (int i = 0; i < _rocketWeapon.Rockets.Length; i++)
            {
                _rocketWeapon.Shoot();
                await UniTask.Delay(TimeSpan.FromSeconds(.1f));
            }
        }
    }
}