using Sources.Controllers;
using Sources.Presentation.Views.Systems.Damageable;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.Presentation.Views.Enemies
{
    public abstract class EnemyView<T> : PresentationViewBase<T>, IEnemyView where T : IPresenter
    {
        [field: SerializeField] public DamageableSystemView DamageableSystemView { get; private set; }

        public Vector3 Position => Transform.position;
        public Vector3 Forward => Transform.forward;
        public bool IsVisible { get; protected set; }

        public void TakeDamage(float damage) =>
            DamageableSystemView.TakeDamage(damage);
    }
}