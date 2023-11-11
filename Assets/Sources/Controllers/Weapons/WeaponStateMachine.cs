using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines;
using Sources.Presentation.Views.Weapons;

namespace Sources.Controllers.Weapons
{
    public class WeaponStateMachine : FiniteStateMachine, IPresenter
    {
        private readonly IWeapon _weapon;
        private readonly ICompositeWeaponView _view;

        public WeaponStateMachine(IWeapon weapon, ICompositeWeaponView view)
        {
            _weapon = weapon;
            _view = view;
        }

        public void Enable()
        {
            _weapon.Shooting += OnShoot;
            Run();
        }

        public void Disable()
        {
            Stop();
            _weapon.Shooting -= OnShoot;
        }

        private void OnShoot() => 
            _view.Shoot(_weapon.BulletId);
    }
}