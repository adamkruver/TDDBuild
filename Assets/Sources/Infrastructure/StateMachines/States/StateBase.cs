using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Infrastructure.StateMachines.States
{
    public abstract class StateBase : IState
    {
        public virtual void Update(float deltaTime)
        {
        }

        public virtual void UpdateFixed(float fixedDeltaTime)
        {
        }

        public virtual void UpdateLate(float deltaTime)
        {
        }

        public abstract void Enter(object payload);

        public abstract void Exit();
    }
}