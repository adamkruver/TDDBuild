using Sources.Frameworks.LiveDatas;
using UnityEngine;

namespace Sources.Domain.Systems
{
    public class MovementSystem
    {
        private const float MaxValue = 3;
        
        private readonly MutableLiveData<float> _speed = new MutableLiveData<float>(0);
        private readonly float _maxValue;

        public MovementSystem() : this(MaxValue)
        {
        }

        public MovementSystem(float maxValue) => 
            _maxValue = maxValue;

        public LiveData<float> Speed => _speed;

        public void SetSpeed(float speed) => 
            _speed.Value = Mathf.Clamp(speed, 0, _maxValue);
    }
}