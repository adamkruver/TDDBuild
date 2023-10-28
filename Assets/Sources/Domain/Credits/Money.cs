using System;
using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Credits
{
    public class Money
    {
        private readonly MutableLiveData<int> _value;

        public Money(int value) =>
            _value = new MutableLiveData<int>(value);
        
        public LiveData<int> Value => _value;

        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            _value.Value += value;
        }
        
        public void Remove(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            _value.Value -= value;
        }

        public bool IsEnough(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            return _value.Value >= value;
        }
    }
}