using Sources.Controllers.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Ui.Systems.Aggressive;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui.Systems
{
    public class AggressiveSystemUiFactory
    {
        private const string PrefabPath = "Ui/Systems/AggressiveSystemUi";
        
        private readonly AggressiveSystemPresenterFactory _aggressiveSystemPresenterFactory;

        public AggressiveSystemUiFactory(AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory) => 
            _aggressiveSystemPresenterFactory = aggressiveSystemPresenterFactory;

        public AggressiveSystemUi Create(AggressiveSystem system)
        {
            AggressiveSystemUi ui = Object.Instantiate(Resources.Load<AggressiveSystemUi>(PrefabPath));
            AggressiveSystemPresenter presenter = _aggressiveSystemPresenterFactory.Create(ui, system);
            ui.Construct(presenter);
            
            return ui;
        }
    }
}