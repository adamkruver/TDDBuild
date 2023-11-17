using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.Scenes
{
    public interface ISceneChangeService
    {
        UniTask ChangeSceneAsync(string sceneName, object payload = null);
    }
}