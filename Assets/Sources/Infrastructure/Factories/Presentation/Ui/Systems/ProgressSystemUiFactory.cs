using Sources.Controllers.Systems;
using Sources.Domain.Systems.Progresses;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Ui.Systems.Progresses;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui.Systems
{
    public class ProgressSystemUiFactory
    {
        private const string PrefabPath = "Ui/Systems/ProgressSystemUi";
        
        private readonly ProgressSystemPresenterFactory _progressSystemPresenterFactory;

        public ProgressSystemUiFactory(ProgressSystemPresenterFactory progressSystemPresenterFactory) =>
            _progressSystemPresenterFactory = progressSystemPresenterFactory;

        public ProgressSystemUi Create(ProgressSystem progressSystem)
        {
            ProgressSystemUi ui = Object.Instantiate(
                Resources.Load<ProgressSystemUi>(PrefabPath)
            );

            ProgressSystemPresenter presenter = _progressSystemPresenterFactory.Create(ui, progressSystem);
            ui.Construct(presenter);

            return ui;
        }
    }
}