using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.Scenes.Events
{
    public interface ISceneServiceEventHandler : IEventHandler
    {
        UniTask Handle();
    }
}