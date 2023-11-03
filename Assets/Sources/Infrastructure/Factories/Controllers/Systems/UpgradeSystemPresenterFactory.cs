using Sources.Controllers.Systems;
using Sources.Domain.Systems.Upgrades;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class UpgradeSystemPresenterFactory
    {
        public UpgradeSystemPresenter Create(IUpgradeSystemUi ui, UpgradeSystem system) => 
            new UpgradeSystemPresenter(ui, system);
    }
}