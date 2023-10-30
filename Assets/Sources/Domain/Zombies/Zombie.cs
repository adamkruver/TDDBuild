using System;
using Sources.Domain.Enemies;
using Sources.Domain.Systems;

namespace Sources.Domain.Zombies
{
    public class Zombie : IEnemy
    {
        public Zombie(MovementSystem movementSystem) =>
            MovementSystem = movementSystem;

        public event Action Died;

        public MovementSystem MovementSystem { get; }

        public void SetSpeed(float speed) =>
            MovementSystem.SetSpeed(speed);
    }
}