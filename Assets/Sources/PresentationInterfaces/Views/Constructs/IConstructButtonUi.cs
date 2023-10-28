using UnityEngine;
using UnityEngine.Events;

namespace Sources.PresentationInterfaces.Views.Constructs
{
    public interface IConstructButtonUi
    {
        void SetIconSprite(Sprite sprite);
        void SetPrice(string price);
        void SetTitle(string title);
        void AddClickListener(UnityAction onClick);
        void RemoveClickListener(UnityAction onClick);
        void Enable();
        void Disable();
    }
}