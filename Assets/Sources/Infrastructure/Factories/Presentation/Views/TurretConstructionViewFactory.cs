using System;
using System.Collections.Generic;
using Sources.Controllers.Constructions;
using Sources.Domain.Turrets;
using Sources.Infrastructure.Factories.Controllers.Constructions;
using Sources.Presentation.Previews.Constructions;
using Sources.PresentationInterfaces.Views.Constructions;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class TurretConstructionViewFactory
    {
        private readonly TurretConstructionPresenterFactory _turretConstructionPresenterFactory;
        private readonly IReadOnlyDictionary<string, TurretConstructionPreview> _constructionViews;

        public TurretConstructionViewFactory(
            TurretConstructionPresenterFactory turretConstructionPresenterFactory,
            IReadOnlyDictionary<string, TurretConstructionPreview> constructionViews
        )
        {
            _constructionViews = constructionViews ?? throw new ArgumentNullException(nameof(constructionViews));
            
            _turretConstructionPresenterFactory = turretConstructionPresenterFactory ??
                                                  throw new ArgumentNullException(
                                                      nameof(turretConstructionPresenterFactory)
                                                  );
        }

        public IConstructionView Create(Turret turret)
        {
            string weaponType = turret.Weapon.GetType().Name;
            
            if (_constructionViews.ContainsKey(weaponType) == false)
                throw new KeyNotFoundException(weaponType);

            TurretConstructionPreview preview = _constructionViews[weaponType];
            
            TurretConstructionPresenter presenter = _turretConstructionPresenterFactory.Create(
                _constructionViews[weaponType], turret
            );
            
            preview.Construct(presenter);
            
            return preview;
        }
    }
}