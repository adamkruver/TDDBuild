using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.InfrastructureInterfaces.Factories.Controllers
{
    public interface IWeaponStateMachineFactory
    {
        IPresenter Create(IWeaponView view, IWeapon weapon, ITargetProvider targetProvider);
    }
}