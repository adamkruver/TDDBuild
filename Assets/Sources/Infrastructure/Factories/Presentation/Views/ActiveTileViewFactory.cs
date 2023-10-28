using Sources.Controllers.Tilemaps;
using Sources.Infrastructure.Factories.Controllers.Tilemaps;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ActiveTileViewFactory
    {
        private const string PrefabPath = "Views/Tilemaps/ActiveTileView";
        
        private readonly ActiveTilePresenterFactory _activeTilePresenterFactory;

        public ActiveTileViewFactory(ActiveTilePresenterFactory activeTilePresenterFactory) =>
            _activeTilePresenterFactory = activeTilePresenterFactory;

        public ActiveTileView Create()
        {
            ActiveTileView view = Object.Instantiate(Resources.Load<ActiveTileView>(PrefabPath));
            ActiveTilePresenter presenter = _activeTilePresenterFactory.Create(view);

            view.Construct(presenter);

            return view;
        }
    }
}