using Sources.Controllers.Zombies;
using Sources.Domain.Zombies;
using Sources.Presentation.Views.Zombies;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombiePresenterFactory
    {
        public ZombiePresenter Create(ZombieView view, Zombie zombie)
        {
            return new ZombiePresenter(view, zombie);
        }
    }
}