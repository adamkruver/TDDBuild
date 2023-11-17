using UnityEngine;
using UnityEngine.Events;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathDrawSystemView
    {
        Vector3 StartPoint { get; }
        Vector3 EndPoint { get; }
        float Distance { get; }
        float SpawnInterval { get; }

        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
    }
}