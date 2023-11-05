using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Presentation.Ui.Systems.Upgrades
{
    public class UpgradeModifierUi : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Image _levelImage;
        [SerializeField] private SpriteContainer _spriteContainer;

        public void AddButtonListener(UnityAction action) => 
            _button.onClick.AddListener(action);
        
        public void RemoveButtonListener(UnityAction action) =>
            _button.onClick.RemoveListener(action);
        
        public void SetLevel(int level) =>
            _levelImage.sprite = _spriteContainer.GetByIndexOrLast(level);
        
        public void SetValue(string value) =>
            _value.text = value;
    }
}