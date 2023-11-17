using Sources.Controllers.Systems.PathDraw;
using Sources.Domain.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;

namespace Sources.Infrastructure.Factories.Controllers.Systems.PathDraw
{
    public class PathDrawSystemPointPresenterFactory
    {
        public PathDrawSystemPointPresenter Create(IPathDrawSystemPointView view, PathDrawSystemPoint point) =>
            new PathDrawSystemPointPresenter(view, point);
    }
}