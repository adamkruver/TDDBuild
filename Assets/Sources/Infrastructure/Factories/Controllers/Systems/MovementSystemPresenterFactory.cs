using Sources.Controllers.Systems;
using Sources.Domain.Systems;
using Sources.Presentation.Views.Systems.Movements;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class MovementSystemPresenterFactory
    {
        public MovementSystemPresenter Create(MovementSystemView view, MovementSystem system)
        {
            return new MovementSystemPresenter(view, system);
        }
    }
}