using Sources.Infrastructure.FiniteStateMachines;

namespace Sources.Controllers.Zombies
{
    public class ZombieStateMachine : FiniteStateMachine, IPresenter
    {
        public void Enable() =>
            Run();

        public void Disable() =>
            Stop();
    }
}