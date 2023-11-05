using System;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Controllers.Systems
{
    public class UpgradeSystemPresenter : PresenterBase
    {
        private const string CooldownFormat = "{0:+0.#;-0.#}";
        private const string MaxFireDistanceFormat = "{0:+0.#;-#.#}";
        private const string DamageFormat = "{0:+0.#;-#.#}";

        private readonly IUpgradeSystemUi _ui;
        private readonly UpgradeSystem _system;
        private readonly LiveData<float> _cooldownValue;
        private readonly LiveData<float> _maxFireDistanceValue;
        private readonly LiveData<float> _damageValue;
        private readonly LiveData<int> _cooldownLevel;
        private readonly LiveData<int> _maxFireDistanceLevel;
        private readonly LiveData<int> _damageLevel;

        public UpgradeSystemPresenter(IUpgradeSystemUi ui, UpgradeSystem system)
        {
            _ui = ui;
            _system = system;

            _cooldownValue = system.Cooldown.Value;
            _cooldownLevel = system.Cooldown.Level;
            _maxFireDistanceValue = system.MaxFireDistance.Value;
            _maxFireDistanceLevel = system.MaxFireDistance.Level;
            _damageValue = system.Damage.Value;
            _damageLevel = system.Damage.Level;
        }

        public override void Enable()
        {
            _cooldownValue.AddListener(OnCooldownValueChanged);
            _cooldownLevel.AddListener(OnCooldownLevelChanged);
            _damageValue.AddListener(OnDamageValueChanged);
            _damageLevel.AddListener(OnDamageLevelChanged);
            _maxFireDistanceValue.AddListener(OnMaxFireDistanceValueChanged);
            _maxFireDistanceLevel.AddListener(OnMaxFireDistanceLevelChanged);
        }

        public override void Disable()
        {
            _cooldownValue.RemoveListener(OnCooldownValueChanged);
            _damageValue.RemoveListener(OnDamageValueChanged);
            _maxFireDistanceValue.RemoveListener(OnMaxFireDistanceValueChanged);
        }

        public void UpgradeDamage() =>
            _system.Damage.Upgrade();

        public void UpgradeCooldown() =>
            _system.Cooldown.Upgrade();

        public void UpgradeMaxFireDistance() =>
            _system.MaxFireDistance.Upgrade();

        private void OnCooldownValueChanged(float cooldown) =>
            _ui.SetCooldownValue(String.Format(CooldownFormat, cooldown));

        private void OnCooldownLevelChanged(int level) =>
            _ui.SetCooldownLevel(level);

        private void OnDamageValueChanged(float damage) =>
            _ui.SetDamageValue(String.Format(DamageFormat, damage));

        private void OnDamageLevelChanged(int level) =>
            _ui.SetDamageLevel(level);

        private void OnMaxFireDistanceValueChanged(float maxFireDistance) =>
            _ui.SetMaxFireDistanceValue(String.Format(MaxFireDistanceFormat, maxFireDistance));

        private void OnMaxFireDistanceLevelChanged(int level) =>
            _ui.SetMaxFireDistanceLevel(level);
    }
}