using System.Linq;
using Sources.Domain.Systems.Aggressive;
using Sources.Frameworks.LiveDatas;
using Sources.Infrastructure.Repositories;
using Sources.PresentationInterfaces.Ui.Systems;
using UnityEngine;

namespace Sources.Controllers.Systems
{
    public class AggressiveSystemPresenter
    {
        private readonly IAggressiveSystemUi _ui;
        private readonly EnemyRepository _enemyRepository;
        private readonly LiveData<int> _progress;
        private readonly LiveData<float> _enemySpeed;
        private readonly LiveData<int> _levelProgress;
        private readonly LiveData<string> _levelTitle;
        private readonly LiveData<float> _levelProgressNormalized;

        public AggressiveSystemPresenter(
            IAggressiveSystemUi ui,
            AggressiveSystem system,
            EnemyRepository enemyRepository
        )
        {
            _ui = ui;
            _enemyRepository = enemyRepository;
            _progress = system.Progress;
            _enemySpeed = system.EnemySpeed;
            _levelProgress = system.LevelProgress;
            _levelTitle = system.LevelTitle;
            _levelProgressNormalized = system.LevelProgressNormalized;
        }

        public void Enable()
        {
            _progress.AddListener(OnProgressChanged);
            _enemySpeed.AddListener(OnEnemySpeedChanged);
            _levelProgress.AddListener(OnLevelProgressChanged);
            _levelTitle.AddListener(OnLevelTitleChanged);
            _levelProgressNormalized.AddListener(OnLevelProgressNormalizedChanged);
        }

        public void Disable()
        {
            _progress.RemoveListener(OnProgressChanged);
            _enemySpeed.RemoveListener(OnEnemySpeedChanged);
            _levelProgress.RemoveListener(OnLevelProgressChanged);
            _levelTitle.RemoveListener(OnLevelTitleChanged);
            _levelProgressNormalized.RemoveListener(OnLevelProgressNormalizedChanged);
        }

        private void OnLevelProgressNormalizedChanged(float normalizedProgress) =>
            _ui.SetLevelProgressNormalized(normalizedProgress);

        private void OnProgressChanged(int progress) =>
            _ui.SetProgress(progress.ToString());

        private void OnLevelTitleChanged(string title) =>
            _ui.SetLevelTitle(title);

        private void OnEnemySpeedChanged(float speed)
        {
            Debug.Log(speed);
            
            _enemyRepository
                .GetAll()
                .ToList()
                .ForEach(enemy => enemy.SetSpeed(speed));
        }

        private void OnLevelProgressChanged(int levelProgress) =>
            _ui.SetLevelProgress(levelProgress.ToString());
    }
}