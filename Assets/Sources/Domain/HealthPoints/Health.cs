using Sources.Frameworks.LiveDatas;
using UnityEngine;

namespace Sources.Domain.HealthPoints
{
    public class Health : IDamageable
    {
        private readonly MutableLiveData<float> _points = new MutableLiveData<float>();
        private readonly MutableLiveData<float> _normalizedPoints = new MutableLiveData<float>();

        private readonly float _maxPoints;

        public Health(float maxPoints)
        {
            _maxPoints = maxPoints;
            SetPoints(maxPoints);
        }

        public LiveData<float> Points => _points;
        public LiveData<float> NormalizedPoints => _normalizedPoints;

        public void TakeDamage(float damage)
        {
            if (damage <= 0)
                return;

            SetPoints(_points.Value - damage);
        }

        public void Heal(float healPoints)
        {
            if (healPoints <= 0)
                return;

            SetPoints(_points.Value + healPoints);
        }

        private void SetPoints(float points)
        {
            points = Mathf.Clamp(points, 0, _maxPoints);
            _points.Value = points;
            _normalizedPoints.Value = points / _maxPoints;
        }
    }
}