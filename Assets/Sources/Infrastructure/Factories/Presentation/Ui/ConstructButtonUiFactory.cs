using Sources.Domain.Constructs;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Ui.Constructs;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui
{
    public class ConstructButtonUiFactory
    {
        private readonly ResourceService _resourceService;
        private readonly ConstructButtonPresenterFactory _constructButtonPresenterFactory;

        public ConstructButtonUiFactory(
            ResourceService resourceService,
            ConstructButtonPresenterFactory constructButtonPresenterFactory
        )
        {
            _resourceService = resourceService;
            _constructButtonPresenterFactory = constructButtonPresenterFactory;
        }

        public ConstructButtonUi Create(ConstructButton constructButton)
        {
            ConstructButtonUi ui = Object.Instantiate(
                _resourceService.Load<ConstructButtonUi>("Ui/Buttons/Constructs/ConstructButtonUi")
            );
            var presenter = _constructButtonPresenterFactory.Create(ui, constructButton);

            ui.Construct(presenter);

            return ui;
        }
    }
}