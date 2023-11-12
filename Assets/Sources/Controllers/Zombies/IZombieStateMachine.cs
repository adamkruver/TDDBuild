namespace Sources.Controllers.Zombies
{
    public interface IZombieStateMachine : IPresenter
    {
        void Enable();
        void Disable();
    }
}