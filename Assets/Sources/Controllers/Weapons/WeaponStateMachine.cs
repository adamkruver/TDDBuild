using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons
{
    public class WeaponStateMachine : FiniteStateMachine, IPresenter
    {
        private readonly IWeapon _weapon;
        private readonly IWeaponView _view;

        public WeaponStateMachine(IWeapon weapon, IWeaponView view)
        {
            _weapon = weapon;
            _view = view;
        }

        public void Enable()
        {
            _weapon.Shooting += OnShoot;
            _weapon.ShootPointChanged += OnShootPointChanged;
            OnShootPointChanged();
            Run();
        }

        public void Disable()
        {
            Stop();
            _weapon.Shooting -= OnShoot;
            _weapon.ShootPointChanged -= OnShootPointChanged;
        }

        private void OnShoot() => 
            _view.Shoot();

        private void OnShootPointChanged() =>  
            _view.SetActiveShootPoint(_weapon.ShootPointIndex);
    }
}