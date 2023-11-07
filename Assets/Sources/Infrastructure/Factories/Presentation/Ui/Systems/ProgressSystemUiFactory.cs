using Sources.Controllers.Systems;
using Sources.Domain.Systems.Progresses;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Ui.Systems.Progresses;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui.Systems
{
    public class ProgressSystemUiFactory
    {
        private readonly ResourceService _resourceService;
        private readonly ProgressSystemPresenterFactory _progressSystemPresenterFactory;

        public ProgressSystemUiFactory(
            ResourceService resourceService,
            ProgressSystemPresenterFactory progressSystemPresenterFactory
        )
        {
            _resourceService = resourceService;
            _progressSystemPresenterFactory = progressSystemPresenterFactory;
        }

        public ProgressSystemUi Create(ProgressSystem progressSystem)
        {
            ProgressSystemUi ui = Object.Instantiate(
                _resourceService.Load<ProgressSystemUi>("Ui/Systems/ProgressSystemUi")
            );

            ProgressSystemPresenter presenter = _progressSystemPresenterFactory.Create(ui, progressSystem);
            ui.Construct(presenter);

            return ui;
        }
    }
}