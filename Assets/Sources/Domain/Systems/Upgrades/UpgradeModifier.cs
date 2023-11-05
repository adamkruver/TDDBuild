using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Systems.Upgrades
{
    public class UpgradeModifier
    {
        private readonly MutableLiveData<float> _value = new MutableLiveData<float>(0);
        private readonly MutableLiveData<int> _level = new MutableLiveData<int>(0);
        private readonly int _maxLevel;
        private readonly float _delta;
        
        public UpgradeModifier(float delta, int maxLevel)
        {
            _delta = delta;
            _maxLevel = maxLevel;
        }
        
        public LiveData<float> Value => _value;
        public LiveData<int> Level => _level;

        public void Upgrade()
        {
            if(_level.Value >= _maxLevel)
                return;

            _level.Value++;
            _value.Value = _delta * _level.Value;
        }
    }
}