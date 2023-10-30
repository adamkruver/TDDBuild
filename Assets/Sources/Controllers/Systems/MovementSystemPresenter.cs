using Sources.Domain.Systems;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Views.Systems;
using Sources.PresentationInterfaces.Views.Systems.Movements;

namespace Sources.Controllers.Systems
{
    public class MovementSystemPresenter
    {
        private readonly IMovementSystemView _view;
        private readonly MovementSystem _system;
        private readonly LiveData<float> _speed;

        public MovementSystemPresenter(IMovementSystemView view, MovementSystem system)
        {
            _view = view;
            _system = system;
            _speed = _system.Speed;
        }

        public void Enable() => 
            _speed.AddListener(OnSpeedChanged);

        public void Disable() => 
            _speed.RemoveListener(OnSpeedChanged);

        public void SetSpeed(float speed) => 
            _system.SetSpeed(speed);

        private void OnSpeedChanged(float speed) => 
            _view.SetSpeed(speed);
    }
}