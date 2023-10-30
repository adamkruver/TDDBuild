using Sources.Domain.Zombies;

namespace Sources.Infrastructure.Factories.Domain.Zombies
{
    public class ZombieFactory
    {
        public Zombie Create()
        {
            return new Zombie();
        }
    }
}