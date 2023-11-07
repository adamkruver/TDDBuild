using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;

namespace Sources.InfrastructureInterfaces.Factories.Controllers
{
    public interface IWeaponStateMachineFactory
    {
        IPresenter Create(ICompositeWeaponView compositeView, IWeapon weapon, ITargetProvider targetProvider);
    }
}