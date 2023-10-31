using Sources.Controllers;
using UnityEngine;

namespace Sources.Presentation.Views
{
    public abstract class PresentationViewBase<T> : MonoBehaviour where T : PresenterBase
    {
        protected T Presenter;
        protected Transform Transform;

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            OnAwake();
        }

        private void OnEnable()
        {
            OnBeforeEnable();
            Presenter?.Enable();
        }

        private void OnDisable()
        {
            Presenter?.Disable();
            OnAfterDisable();
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void Construct(T presenter)
        {
            Hide();
            Presenter = presenter;
            Show();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnBeforeEnable()
        {
        }

        protected virtual void OnAfterDisable()
        {
        }
    }
}