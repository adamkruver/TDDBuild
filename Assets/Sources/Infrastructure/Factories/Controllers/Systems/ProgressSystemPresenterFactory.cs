using Sources.Controllers.Systems;
using Sources.Domain.Systems.Progresses;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class ProgressSystemPresenterFactory
    {
        public ProgressSystemPresenter Create(IProgressSystemUi ui, ProgressSystem system) =>
            new ProgressSystemPresenter(ui, system);
    }
}