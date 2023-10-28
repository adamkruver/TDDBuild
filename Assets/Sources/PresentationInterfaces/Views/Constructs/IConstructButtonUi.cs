using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Constructs
{
    public interface IConstructButtonUi
    {
        void SetIconSprite(Sprite sprite);
        void SetPrice(string price);
        void SetTitle(string title);
    }
}