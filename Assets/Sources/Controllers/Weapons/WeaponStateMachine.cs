using Sources.Infrastructure.FiniteStateMachines;

namespace Sources.Controllers.Weapons
{
    public class WeaponStateMachine : FiniteStateMachine, IPresenter
    {
        public void Enable() =>
            Run();

        public void Disable() =>
            Stop();
    }
}