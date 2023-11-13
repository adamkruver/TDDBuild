using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class RocketShootState : FiniteStateBase
    {
        private readonly RocketTwiceGun _weapon;
        private readonly ICompositeWeaponView _view;
        private readonly ITargetProvider _targetProvider;

        public RocketShootState(RocketTwiceGun weapon, ICompositeWeaponView view, ITargetProvider targetProvider)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _view = view;
            _targetProvider = targetProvider;
        }

        protected override void OnEnter()
        {
            _weapon.Rocket.SetEnemy(_targetProvider.GetTarget());
            Shoot();
        }

        private async UniTask Shoot()
        {
            foreach (var bullet in _weapon.Bullets)
            {
                _weapon.Shoot();
                await UniTask.Delay(TimeSpan.FromSeconds(.1f));
            }
        }
    }
}