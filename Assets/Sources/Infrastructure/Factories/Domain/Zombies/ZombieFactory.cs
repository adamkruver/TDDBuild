using Sources.Domain.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Repositories;

namespace Sources.Infrastructure.Factories.Domain.Zombies
{
    public class ZombieFactory
    {
        private readonly EnemyRepository _enemyRepository;
        private readonly AggressiveSystem _aggressiveSystem;

        public ZombieFactory(EnemyRepository enemyRepository, AggressiveSystem aggressiveSystem)
        {
            _enemyRepository = enemyRepository;
            _aggressiveSystem = aggressiveSystem;
        }

        public Zombie Create()
        {
            float zombieMaxHealthPoints = 100;
            
            MovementSystem movementSystem = new MovementSystem();

            Zombie zombie = new Zombie(zombieMaxHealthPoints, movementSystem);

            _enemyRepository.Add(zombie);
            movementSystem.SetSpeed(_aggressiveSystem.EnemySpeed.Value);

            return zombie;
        }
    }
}