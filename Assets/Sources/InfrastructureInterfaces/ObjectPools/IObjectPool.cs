using Sources.Presentation.Components;

namespace Sources.InfrastructureInterfaces.ObjectPools
{
    public interface IObjectPool
    {
        T Get<T>() where T : PoolableBehaviour;
    }
}