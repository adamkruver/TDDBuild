using Sources.Controllers.Constructs;
using Sources.PresentationInterfaces.Views.Constructs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Presentation.Ui.Constructs
{
    public class ConstructButtonUi : MonoBehaviour, IConstructButtonUi
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _frame;
        [SerializeField] private Button _button;
        [SerializeField] private TextUi _title;
        [SerializeField] private TextUi _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Color32 _activeColor;
        [SerializeField] private Color32 _inactiveColor;

        private ConstructButtonPresenter _constructButtonPresenter;

        private void OnEnable() =>
            _constructButtonPresenter?.Enable();

        private void OnDisable() =>
            _constructButtonPresenter?.Disable();

        public void Enable()
        {
            _background.color = _activeColor;
            _frame.color = _activeColor;
            _title.Activate();
            _price.Activate();
        }

        public void Disable()
        {
            _background.color = _inactiveColor;
            _frame.color = _inactiveColor;
            _title.Deactivate();
            _price.Deactivate();
        }

        public void AddClickListener(UnityAction onClick) =>
            _button.onClick.AddListener(onClick);

        public void RemoveClickListener(UnityAction onClick) =>
            _button.onClick.RemoveListener(onClick);

        public void Construct(ConstructButtonPresenter constructButtonPresenter)
        {
            gameObject.SetActive(false);
            _constructButtonPresenter = constructButtonPresenter;
            gameObject.SetActive(true);
        }

        public void SetIconSprite(Sprite sprite) =>
            _icon.sprite = sprite;

        public void SetPrice(string price) =>
            _price.SetText(price);

        public void SetTitle(string title) =>
            _title.SetText(title);
    }
}