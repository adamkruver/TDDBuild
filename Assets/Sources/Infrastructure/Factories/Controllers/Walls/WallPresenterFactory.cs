using Sources.Controllers.Walls;
using Sources.Domain.Walls;
using Sources.PresentationInterfaces.Views.Walls;

namespace Sources.Infrastructure.Factories.Controllers.Walls
{
    public class WallPresenterFactory
    {
        public WallPresenter Create(IWallView view, Wall wall) => 
            new WallPresenter(view, wall);
    }
}