using Sources.Controllers.Zombies.StateMachines;
using Sources.Controllers.Zombies.StateMachines.States;
using Sources.Presentation.Views.Zombies;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombieAfterLifeStateMachineFactory
    {
        public ZombieAfterLifeStateMachine Create(ZombieView view)
        {
            ZombieAfterLifeStateMachine stateMachine = new ZombieAfterLifeStateMachine();

            CreateStates(view, stateMachine);

            return stateMachine;
        }

        private void CreateStates(ZombieView view, ZombieAfterLifeStateMachine stateMachine)
        {
            DestroyState destroyState = new DestroyState(view);
            DecayState decayState = new DecayState(destroyState, stateMachine, view);
            FallState fallState = new FallState(decayState, stateMachine, view);
            
            stateMachine.SetFirstState(fallState);
        }
    }
}