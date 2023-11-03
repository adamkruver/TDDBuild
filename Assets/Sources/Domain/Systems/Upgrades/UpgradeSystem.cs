using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Systems.Upgrades
{
    public class UpgradeSystem
    {
        private readonly MutableLiveData<float> _cooldown = new MutableLiveData<float>(0);
        private readonly MutableLiveData<float> _maxFireDistance = new MutableLiveData<float>(0);
        private readonly MutableLiveData<float> _damage = new MutableLiveData<float>(0);

        public UpgradeSystem(float cooldown = 0, float maxFireDistance = 0, float damage = 0)
        {
            SetCooldown(cooldown);
            SetMaxFireDistance(maxFireDistance);
            SetDamage(damage);
        }

        public LiveData<float> Cooldown => _cooldown;
        public LiveData<float> MaxFireDistance => _maxFireDistance;
        public LiveData<float> Damage => _damage;

        public void UpgradeCooldown(float cooldown) =>
            SetCooldown(_cooldown.Value + cooldown);

        public void UpgradeMaxFireDistance(float maxFireDistance) =>
            SetMaxFireDistance(_maxFireDistance.Value + maxFireDistance);

        public void UpgradeDamage(float damage) =>
            SetDamage(_damage.Value + damage);

        private void SetCooldown(float cooldown) =>
            _cooldown.Value = cooldown;

        private void SetMaxFireDistance(float maxFireDistance) =>
            _maxFireDistance.Value = maxFireDistance;

        private void SetDamage(float damage) =>
            _damage.Value = damage;
    }
}