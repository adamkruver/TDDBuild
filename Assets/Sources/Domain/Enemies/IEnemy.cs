using System;

namespace Sources.Domain.Enemies
{
    public interface IEnemy
    {
        event Action Died;
        void SetSpeed(float speed);
    }
}