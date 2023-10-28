using TMPro;
using UnityEngine;

namespace Sources.Presentation.Ui
{
    public class TextUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color32 _activeColor;
        [SerializeField] private Color32 _inactiveColor;

        public void SetText(string text) =>
            _text.text = text;

        public void Activate() => 
            _text.faceColor = _activeColor;

        public void Deactivate() => 
            _text.faceColor = _inactiveColor;
    }
}