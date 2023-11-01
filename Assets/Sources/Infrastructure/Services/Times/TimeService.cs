using Sources.InfrastructureInterfaces.Services.Times;
using UnityEngine;

namespace Sources.Infrastructure.Services.Times
{
    public class TimeService : ITimeService
    {
        public float Time => UnityEngine.Time.unscaledTime;
    }
}