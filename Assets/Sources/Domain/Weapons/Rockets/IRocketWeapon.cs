using Sources.Domain.Projectiles;
using UnityEngine;

namespace Sources.Domain.Weapons.Rockets
{
    public interface IRocketWeapon : IWeapon
    {
        Rocket[] Rockets { get; }
        bool HasNoRockets { get; }

        void SetRockets(Rocket[] rockets);
        void SetDestination(Vector3 position);
    }
}