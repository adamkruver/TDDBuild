using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.Scenes
{
    public interface ISceneChangeService
    {
        UniTask ChangeStateAsync(string sceneName, object payload = null);
    }
}