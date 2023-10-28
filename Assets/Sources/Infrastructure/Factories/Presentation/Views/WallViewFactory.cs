using Sources.Controllers.Walls;
using Sources.Domain.Walls;
using Sources.Infrastructure.Factories.Controllers.Walls;
using Sources.Presentation.Views.Walls;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WallViewFactory
    {
        private const string PrefabPath = "Views/Walls/WallView";

        private readonly WallPresenterFactory _wallPresenterFactory;

        public WallViewFactory(WallPresenterFactory wallPresenterFactory) =>
            _wallPresenterFactory = wallPresenterFactory;

        public WallView Create(Wall wall, Vector2Int position)
        {
            WallView view = Object.Instantiate(Resources.Load<WallView>(PrefabPath));
            WallPresenter wallPresenter = _wallPresenterFactory.Create(view, wall);
            
            view.SetPosition(position);

            return view;
        }
    }
}