using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.Damageable;

namespace Sources.Presentation.Views.Systems.Damageable
{
    public class DamageableSystemView : PresentationViewBase<DamageableSystemPresenter>, IDamageableSystemView
    {
        public void TakeDamage(float damage) =>
            Presenter?.TakeDamage(damage);
    }
}