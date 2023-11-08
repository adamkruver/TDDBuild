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
        private readonly LiveData<int> _levelProgress;
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
            _levelProgress = system.LevelProgress;
            _levelProgressNormalized = system.LevelProgressNormalized;
        }

        public void Enable()
        {
            _progress.AddListener(OnProgressChanged);
            _levelProgress.AddListener(OnLevelProgressChanged);
            _levelProgressNormalized.AddListener(OnLevelProgressNormalizedChanged);
        }

        public void Disable()
        {
            _progress.RemoveListener(OnProgressChanged);
            _levelProgress.RemoveListener(OnLevelProgressChanged);
            _levelProgressNormalized.RemoveListener(OnLevelProgressNormalizedChanged);
        }

        private void OnLevelProgressNormalizedChanged(float normalizedProgress) =>
            _ui.SetLevelProgressNormalized(normalizedProgress);

        private void OnProgressChanged(int progress) =>
            _ui.SetProgress(progress.ToString());

        private void OnLevelProgressChanged(int levelProgress) =>
            _ui.SetLevelProgress(levelProgress.ToString());
    }
}