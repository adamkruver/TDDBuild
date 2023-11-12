using Sources.Infrastructure.FiniteStateMachines;

namespace Sources.Controllers.Zombies.FiniteStateMachines
{
    public class ZombieStateMachine : FiniteStateMachine, IZombieStateMachine
    {
        public void Enable() =>
            Run();

        public void Disable() =>
            Stop();
    }
}