using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Presentation.Views.Bootstrap
{
    public class CurtainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 1;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _canvasGroup.alpha = 0;
        }

        public async UniTask Show() =>
            await Fade(0, 1);

        public async UniTask Hide() => 
            await Fade(1, 0);

        private async UniTask Fade(float startAlfa, float endAlfa)
        {
            _canvasGroup.alpha = startAlfa;

            while (Mathf.Abs(_canvasGroup.alpha - endAlfa) > 0.01f)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, endAlfa, Time.deltaTime / _duration);

                await UniTask.Yield();
            }

            _canvasGroup.alpha = endAlfa;
        }
    }
}