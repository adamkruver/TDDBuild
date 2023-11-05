using Sources.Controllers.Systems;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Ui.Systems;
using UnityEngine;

namespace Sources.Presentation.Ui.Systems.Upgrades
{
    public class UpgradeSystemUi : PresentationViewBase<UpgradeSystemPresenter>, IUpgradeSystemUi
    {
        [SerializeField] UpgradeModifierUi _cooldown;
        [SerializeField] UpgradeModifierUi _maxFireDistance;
        [SerializeField] UpgradeModifierUi _damage;

        protected override void OnBeforeEnable()
        {
            if (Presenter is null)
                return;

            _cooldown.AddButtonListener(Presenter.UpgradeCooldown);
            _maxFireDistance.AddButtonListener(Presenter.UpgradeMaxFireDistance);
            _damage.AddButtonListener(Presenter.UpgradeDamage);
        }

        protected override void OnAfterDisable()
        {
            if (Presenter is null)
                return;

            _cooldown.RemoveButtonListener(Presenter.UpgradeCooldown);
            _maxFireDistance.RemoveButtonListener(Presenter.UpgradeMaxFireDistance);
            _damage.RemoveButtonListener(Presenter.UpgradeDamage);
        }

        public void SetCooldownValue(string cooldown) =>
            _cooldown.SetValue(cooldown);

        public void SetCooldownLevel(int level) =>
            _cooldown.SetLevel(level);

        public void SetMaxFireDistanceValue(string maxFireDistance) =>
            _maxFireDistance.SetValue(maxFireDistance);

        public void SetMaxFireDistanceLevel(int level) =>
            _maxFireDistance.SetLevel(level);

        public void SetDamageValue(string damage) =>
            _damage.SetValue(damage);

        public void SetDamageLevel(int level) =>
            _damage.SetLevel(level);
    }
}