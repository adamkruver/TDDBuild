using Sources.Controllers.Systems.PathDraw;
using Sources.Infrastructure.Factories.Presentation.Systems.PathDraw;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;

namespace Sources.Infrastructure.Factories.Controllers.Systems.PathDraw
{
    public class PathDrawSystemPresenterFactory
    {
        public PathDrawSystemPresenter Create(IPathDrawSystemView systemView, NavMeshService navMeshService,
            PathDrawSystemPointViewFactory pathDrawSystemPointViewFactory) =>
            new PathDrawSystemPresenter(systemView, navMeshService, pathDrawSystemPointViewFactory);
    }
}