using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.View
{
    public interface IPathPointViewFactory
    {
        public IPathPointView Create(Vector3 spawnPosition);
    }
}