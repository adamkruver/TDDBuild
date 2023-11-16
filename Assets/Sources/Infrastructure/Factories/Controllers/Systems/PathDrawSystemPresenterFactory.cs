using Sources.Controllers.Systems;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class PathDrawSystemPresenterFactory
    {
        public PathDrawSystemPresenter Create(IPathDrawView view, NavMeshService navMeshService,
            IPathPointViewFactory pathPointViewFactory) =>
            new PathDrawSystemPresenter(view, navMeshService, pathPointViewFactory);
    }
}