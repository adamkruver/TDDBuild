using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.InfrastructureInterfaces.Services.Weapons
{
    public interface IWeaponService
    {
        void UpdateLookDirectionWithPredict(IEnemyView enemy, float rotationSpeed, float gunpointXOffset);
        bool HasLockedTarget(IEnemyView enemyView, float gunpointXOffset);
    }
}