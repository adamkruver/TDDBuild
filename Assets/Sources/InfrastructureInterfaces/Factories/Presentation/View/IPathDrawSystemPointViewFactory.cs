using Sources.Presentation.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.View
{
    public interface IPathDrawSystemPointViewFactory
    {
        PathDrawSystemPointView Create(Vector3 position, Vector3 direction);
    }
}