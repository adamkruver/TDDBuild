using Sources.Controllers.Systems;
using Sources.Domain.Systems.Upgrades;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Ui.Systems.Upgrades;

namespace Sources.Infrastructure.Factories.Presentation.Ui.Systems
{
    public class UpgradeSystemUiFactory
    {
        private readonly UpgradeSystemPresenterFactory _upgradeSystemPresenterFactory;

        public UpgradeSystemUiFactory(UpgradeSystemPresenterFactory upgradeSystemPresenterFactory) =>
            _upgradeSystemPresenterFactory = upgradeSystemPresenterFactory;

        public UpgradeSystemUi Create(UpgradeSystemUi ui, UpgradeSystem system)
        {
            UpgradeSystemPresenter presenter = _upgradeSystemPresenterFactory.Create(ui, system);

            ui.Construct(presenter);

            return ui;
        }
    }
}