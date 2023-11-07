using Sources.Controllers.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Ui.Systems.Aggressive;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui.Systems
{
    public class AggressiveSystemUiFactory
    {
        private readonly ResourceService _resourceService;
        private readonly AggressiveSystemPresenterFactory _aggressiveSystemPresenterFactory;

        public AggressiveSystemUiFactory(
            ResourceService resourceService,
            AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory
        )
        {
            _resourceService = resourceService;
            _aggressiveSystemPresenterFactory = aggressiveSystemPresenterFactory;
        }

        public AggressiveSystemUi Create(AggressiveSystem system)
        {
            AggressiveSystemUi ui = Object.Instantiate(
                _resourceService.Load<AggressiveSystemUi>("Ui/Systems/AggressiveSystemUi")
            );
            AggressiveSystemPresenter presenter = _aggressiveSystemPresenterFactory.Create(ui, system);
            ui.Construct(presenter);

            return ui;
        }
    }
}