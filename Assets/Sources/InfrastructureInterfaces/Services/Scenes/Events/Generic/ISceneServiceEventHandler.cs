using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.Scenes.Events.Generic
{
    public interface ISceneServiceEventHandler<T> : IEventHandler
    {
        UniTask Handle(T payload);
    }
}