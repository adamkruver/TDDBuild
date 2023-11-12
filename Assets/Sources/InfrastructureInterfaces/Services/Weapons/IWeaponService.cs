using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Weapons
{
    public interface IWeaponService
    {
        void UpdateLookDirectionWithPredict(IEnemyView enemy, float rotationSpeed, float gunpointXOffset, Vector3 shootPoint);
        bool HasLockedTarget(IEnemyView enemyView, float gunpointXOffset, Vector3 shootPoint);
    }
}