using Sources.Controllers.Constructs;
using Sources.PresentationInterfaces.Views.Constructs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Constructs
{
    public class ConstructButtonUi : MonoBehaviour, IConstructButtonUi
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Image _icon;

        private ConstructButtonPresenter _constructButtonPresenter;

        public void AddClickListener(UnityAction onClick) =>
            _button.onClick.AddListener(onClick);

        public void RemoveClickListener(UnityAction onClick) =>
            _button.onClick.RemoveListener(onClick);

        public void Construct(ConstructButtonPresenter constructButtonPresenter) =>
            _constructButtonPresenter = constructButtonPresenter;

        public void SetIconSprite(Sprite sprite) =>
            _icon.sprite = sprite;

        public void SetPrice(string price) =>
            _price.text = price;

        public void SetTitle(string title) =>
            _title.text = title;
    }
}