using Sources.Domain.HealthPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Enemies
{
    public interface IEnemyView : IDamageable
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        bool IsVisible { get; }
    }
}