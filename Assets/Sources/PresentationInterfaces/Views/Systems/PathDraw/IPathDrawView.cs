using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathDrawView
    {
        Vector3 StartPoint { get; }
        Vector3 EndPoint { get; }
        float Interval { get; }

        void Append(IPathPointView pathPointView);
        void ShowPoints();
        void HidePoints();
        void Clear();
    }
}