using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.InfrastructureInterfaces.Services.Weapons
{
    public interface ITargetProvider
    {
        IEnemyView GetTarget();
    }
}