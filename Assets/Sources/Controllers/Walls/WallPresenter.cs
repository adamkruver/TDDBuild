using Sources.Domain.Walls;
using Sources.PresentationInterfaces.Views.Walls;

namespace Sources.Controllers.Walls
{
    public class WallPresenter
    {
        private readonly IWallView _view;
        private readonly Wall _wall;

        public WallPresenter(IWallView view, Wall wall)
        {
            _view = view;
            _wall = wall;
        }
    }
}