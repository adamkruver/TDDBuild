using Sources.Domain.HealthPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.Damageable
{
    public interface IDamageableSystemView : IDamageable
    {
        Vector3 LastHitDirection { get; }
    }
}