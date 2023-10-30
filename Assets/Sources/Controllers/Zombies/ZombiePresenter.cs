using Sources.Domain.Zombies;
using Sources.Presentation.Views.Zombies;
using Sources.PresentationInterfaces.Views.Zombies;

namespace Sources.Controllers.Zombies
{
    public class ZombiePresenter
    {
        private readonly IZombieView _view;
        private readonly Zombie _zombie;

        public ZombiePresenter(IZombieView view, Zombie zombie)
        {
            _view = view;
            _zombie = zombie;
        }
    }
}