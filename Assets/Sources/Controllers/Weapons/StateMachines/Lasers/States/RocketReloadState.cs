using System;
using System.Linq;
using JetBrains.Annotations;
using Sources.Domain.Projectiles;
using Sources.Domain.Weapons.Rockets;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class RocketReloadState : FiniteStateBase
    {
        private readonly RocketFactory _rocketFactory;
        private readonly IRocketViewFactory _rocketViewFactory;
        private readonly IWeaponView _view;
        private readonly IRocketWeapon _rocketWeapon;

        public RocketReloadState(
            IWeaponView view,
            IRocketWeapon rocketWeapon,
            RocketFactory rocketFactory,
            IRocketViewFactory rocketViewFactory
        )
        {
            _rocketFactory = rocketFactory ?? throw new ArgumentNullException(nameof(rocketFactory));
            _rocketViewFactory = rocketViewFactory ?? throw new ArgumentNullException(nameof(rocketViewFactory));
            _view = view;
            _rocketWeapon = rocketWeapon;
        }

        protected override void OnEnter()
        {
            Reload();
        }

        private void Reload()
        {
            Rocket[] rockets = Enumerable.Repeat(_rocketFactory.Create(), _view.ShootPoints.Length).ToArray();
            _rocketWeapon.SetRockets(rockets);

            for (int i = 0; i < _rocketWeapon.Rockets.Length; i++)
                _view.ShootPoints[i].SetProjectile(_rocketViewFactory.Create(_rocketWeapon.Rockets[i]));
        }
    }
}