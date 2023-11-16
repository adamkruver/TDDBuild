using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.Presentation.Views.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class PathPointViewFactory : IPathPointViewFactory
    {
        public IPathPointView Create(Vector3 spawnPosition)
        {
            PathPointView pathPointView = Object
                .Instantiate(Resources.Load<PathPointView>("Views/PathDraw/PathPointView"), spawnPosition,
                    Quaternion.identity);
            pathPointView.Initialize();

            return pathPointView;
        }
    }
}