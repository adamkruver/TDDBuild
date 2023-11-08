using System;

namespace Sources.Domain.Enemies
{
    public interface IEnemy : IDisposable
    {
        event Action Died;
    }
}