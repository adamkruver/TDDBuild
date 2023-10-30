using Sources.Controllers.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Views.Systems.Aggressive;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Systems
{
    public class AggressiveSystemViewFactory
    {
        private const string PrefabPath = "Views/Systems/AggressiveSystemView";
        
        private readonly AggressiveSystemPresenterFactory _aggressiveSystemPresenterFactory;

        public AggressiveSystemViewFactory(AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory) => 
            _aggressiveSystemPresenterFactory = aggressiveSystemPresenterFactory;

        public AggressiveSystemView Create(AggressiveSystem system)
        {
            AggressiveSystemView view = Object.Instantiate(Resources.Load<AggressiveSystemView>(PrefabPath));
            AggressiveSystemPresenter presenter = _aggressiveSystemPresenterFactory.Create(view, system);
            view.Construct(presenter);
            
            return view;
        }
    }
}