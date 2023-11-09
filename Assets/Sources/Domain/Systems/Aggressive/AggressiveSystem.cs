using Sources.Frameworks.LiveDatas;
using UnityEngine;

namespace Sources.Domain.Systems.Aggressive
{
    public class AggressiveSystem
    {
        private readonly MutableLiveData<int> _progress = new MutableLiveData<int>(0);
        private readonly MutableLiveData<int> _levelProgress = new MutableLiveData<int>(0);
        private readonly MutableLiveData<float> _levelProgressNormalized = new MutableLiveData<float>(0);
        private readonly AggressiveLevel[] _levels;

        private int _levelIndex = 0;

        public AggressiveSystem(AggressiveLevelCollection levelCollection)
        {
            _levels = levelCollection.Levels.ToArray();
            AdditionalHealth = 0;
            OnLevelUpReached();
        }

        public float AdditionalHealth { get; private set; }

        public LiveData<int> Progress => _progress;
        public LiveData<int> LevelProgress => _levelProgress;
        public LiveData<float> LevelProgressNormalized => _levelProgressNormalized;

        public void AddProgress(int progress)
        {
            _progress.Value += progress;

            int levelProgress = _levelProgress.Value;
            levelProgress += progress;

            int levelUpProgress = _levels[_levelIndex].UpProgress;

            if (levelProgress >= levelUpProgress)
            {
                levelProgress -= levelUpProgress;

                if (_levelIndex < _levels.Length - 1)
                    _levelIndex++;

                levelUpProgress = _levels[_levelIndex].UpProgress;
                OnLevelUpReached();
            }

            _levelProgress.Value = Mathf.Min(levelProgress, _levels[_levelIndex].UpProgress);
            _levelProgressNormalized.Value = Mathf.Min((float)levelProgress / levelUpProgress, 1);
        }

        private void OnLevelUpReached() => 
        AdditionalHealth += _levels[_levelIndex].AdditionalHealth;
    }
}