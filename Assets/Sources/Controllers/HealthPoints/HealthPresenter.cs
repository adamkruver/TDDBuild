using Sources.Domain.HealthPoints;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Views.HealthPoints;

namespace Sources.Controllers.HealthPoints
{
    public class HealthPresenter : PresenterBase
    {
        private readonly IHealthView _view;
        private readonly LiveData<float> _health;

        public HealthPresenter(IHealthView view, Health health)
        {
            _view = view;
            _health = health.NormalizedPoints;
        }

        public override void Enable() =>
            _health.AddListener(OnHealthNormalizedPointsChanged);

        public override void Disable() =>
            _health.RemoveListener(OnHealthNormalizedPointsChanged);

        private void OnHealthNormalizedPointsChanged(float health) =>
            _view.SetNormalizedValue(health);
    }
}