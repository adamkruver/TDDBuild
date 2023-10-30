using Sources.Frameworks.LiveDatas;
using UnityEngine;

namespace Sources.Domain.Systems.Aggressive
{
    public class AggressiveSystem
    {
        private readonly MutableLiveData<int> _progress = new MutableLiveData<int>(0);
        private readonly MutableLiveData<int> _levelProgress = new MutableLiveData<int>(0);
        private readonly MutableLiveData<float> _levelProgressNormalized = new MutableLiveData<float>(0);
        private readonly MutableLiveData<string> _levelTitle;
        private readonly MutableLiveData<float> _enemySpeed;
        private readonly AggressiveLevel[] _levels;
        
        private int _levelIndex = 0;

        public AggressiveSystem(AggressiveLevelCollection levelCollection)
        {
            _levels = levelCollection.Levels.ToArray();
            _levelTitle = new MutableLiveData<string>(_levels[0].Title);
            _enemySpeed = new MutableLiveData<float>(_levels[0].EnemySpeed);
        }

        public LiveData<string> LevelTitle => _levelTitle;
        public LiveData<float> EnemySpeed => _enemySpeed;
        public LiveData<int> Progress => _progress;
        public LiveData<int> LevelProgress => _levelProgress;
        public LiveData<float> LevelProgressNormalized => _levelProgressNormalized;

        public void AddProgress(int progress)
        {
            _progress.Value += progress;

            int levelProgress = _levelProgress.Value;
            levelProgress += progress;

            int levelUpProgress = _levels[_levelIndex].UpProgress;

            if (levelProgress >= levelUpProgress && _levelIndex < _levels.Length - 1)
            {
                levelProgress -= levelUpProgress;
                _levelIndex++;
                levelUpProgress = _levels[_levelIndex].UpProgress;
            }

            _levelProgress.Value = Mathf.Min(levelProgress, _levels[_levelIndex].UpProgress);
            _levelProgressNormalized.Value = Mathf.Min((float)levelProgress / levelUpProgress, 1);
            _enemySpeed.Value = _levels[_levelIndex].EnemySpeed;
            _levelTitle.Value = _levels[_levelIndex].Title;
        }
    }
}