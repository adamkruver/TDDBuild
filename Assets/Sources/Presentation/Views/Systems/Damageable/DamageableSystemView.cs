using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.Damageable;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.Damageable
{
    public class DamageableSystemView : PresentationViewBase<DamageableSystemPresenter>, IDamageableSystemView
    {
        public Vector3 LastHitDirection { get; private set; }

        public void TakeDamage(float damage, Vector3 direction)
        {
            LastHitDirection = direction;
            Presenter?.TakeDamage(damage, direction);
        }
    }
}