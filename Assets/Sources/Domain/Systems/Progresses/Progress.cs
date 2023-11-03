using System;

namespace Sources.Domain.Systems.Progresses
{
    public class Progress
    {
        public Progress(long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public long Value { get; }

        public static Progress operator +(Progress first, Progress second) =>
            new Progress(first.Value + second.Value);
    }
}