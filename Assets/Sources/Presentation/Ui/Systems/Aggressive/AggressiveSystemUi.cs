using DG.Tweening;
using Sources.Controllers.Systems;
using Sources.Presentation.Ui.Elements;
using Sources.PresentationInterfaces.Ui.Systems;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.Ui.Systems.Aggressive
{
    public class AggressiveSystemUi : MonoBehaviour, IAggressiveSystemUi
    {
        [SerializeField] private TextMeshProUGUI _progress;
        [SerializeField] private TextMeshProUGUI _levelTitle;
        [SerializeField] private TextMeshProUGUI _levelProgress;
        [SerializeField] private CircleSlider _progressSlider;

        private AggressiveSystemPresenter _presenter;

        private void OnEnable() =>
            _presenter?.Enable();

        private void OnDisable() =>
            _presenter?.Disable();

        public void SetLevelProgressNormalized(float normalizedProgress) =>
            _progressSlider.SetNormalizedValue(normalizedProgress);

        public void SetProgress(string progress) =>
            _progress.text = progress;

        public void SetLevelTitle(string title) =>
            _levelTitle.text = title;

        public void SetLevelProgress(string levelProgress)
        {
            _levelProgress.text = levelProgress;
            
            DOTween.Sequence()
                .Append(_levelProgress.transform.DOScale(Vector3.one * 1.5f, .1f))
                .Append(_levelProgress.transform.DOScale(Vector3.one, .1f));
        }

        public void Construct(AggressiveSystemPresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }
    }
}