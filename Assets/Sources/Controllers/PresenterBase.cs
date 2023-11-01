namespace Sources.Controllers
{
    public abstract class PresenterBase : IPresenter
    {
        public virtual void Enable()
        {
        }

        public virtual void Disable()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void UpdateFixed(float fixedDeltaTime)
        {
        }

        public virtual void UpdateLate(float deltaTime)
        {
        }
    }
}