using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Controllers.Systems
{
    public class UpgradeSystemPresenter : PresenterBase
    {
        private readonly IUpgradeSystemUi _ui;
        private readonly UpgradeSystem _system;
        private readonly LiveData<float> _cooldown;
        private readonly LiveData<float> _maxFireDistance;
        private readonly LiveData<float> _damage;

        public UpgradeSystemPresenter(IUpgradeSystemUi ui, UpgradeSystem system)
        {
            _ui = ui;
            _system = system;

            _cooldown = system.Cooldown;
            _maxFireDistance = system.MaxFireDistance;
            _damage = system.Damage;
        }

        public override void Enable()
        {
            _cooldown.AddListener(OnCooldownChanged);
            _damage.AddListener(OnDamageChanged);
            _maxFireDistance.AddListener(OnMaxFireDistanceChanged);
        }

        public override void Disable()
        {
            _cooldown.RemoveListener(OnCooldownChanged);
            _damage.RemoveListener(OnDamageChanged);
            _maxFireDistance.RemoveListener(OnMaxFireDistanceChanged);
        }

        public void UpgradeDamage() =>
            _system.UpgradeDamage(.2f);

        public void UpgradeCooldown() =>
            _system.UpgradeCooldown(-.05f);

        public void UpgradeMaxFireDistance() =>
            _system.UpgradeMaxFireDistance(.5f);

        private void OnCooldownChanged(float cooldown) =>
            _ui.SetCooldown(cooldown.ToString());

        private void OnDamageChanged(float damage) =>
            _ui.SetDamage(damage.ToString());

        private void OnMaxFireDistanceChanged(float maxFireDistance) =>
            _ui.SetMaxFireDistance(maxFireDistance.ToString());
    }
}