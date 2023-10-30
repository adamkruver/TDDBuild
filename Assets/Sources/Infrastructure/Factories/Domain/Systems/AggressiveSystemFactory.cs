using Sources.Domain.Systems.Aggressive;

namespace Sources.Infrastructure.Factories.Domain.Systems
{
    public class AggressiveSystemFactory
    {
        public AggressiveSystem Create(AggressiveLevelCollection collection) =>
            new AggressiveSystem(collection);
    }
}