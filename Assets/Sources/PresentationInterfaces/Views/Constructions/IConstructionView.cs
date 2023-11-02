using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Constructions
{
    public interface IConstructionView
    {
        void SetPosition(Vector3 worldPosition);
        void Build(Vector3 worldPosition);
        void Show();
        void Hide();
    }
}