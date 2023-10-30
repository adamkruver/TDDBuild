using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.Aggressive;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Systems.Aggressive
{
    public class AggressiveSystemView : MonoBehaviour, IAggressiveSystemView
    {
        [SerializeField] private TextMeshProUGUI _progress;
        [SerializeField] private TextMeshProUGUI _levelTitle;
        [SerializeField] private TextMeshProUGUI _levelProgress;
        [SerializeField] private Slider _progressSlider;

        private AggressiveSystemPresenter _presenter;

        private void OnEnable() =>
            _presenter?.Enable();

        private void OnDisable() =>
            _presenter?.Disable();

        public void SetLevelProgressNormalized(float normalizedProgress) =>
            _progressSlider.value = normalizedProgress;

        public void SetProgress(string progress) =>
            _progress.text = progress;

        public void SetLevelTitle(string title) =>
            _levelTitle.text = title;

        public void SetLevelProgress(string levelProgress) =>
            _levelProgress.text = levelProgress;

        public void Construct(AggressiveSystemPresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }
    }
}