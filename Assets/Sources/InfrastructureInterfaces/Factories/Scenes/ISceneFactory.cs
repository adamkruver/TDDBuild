using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;

namespace Sources.InfrastructureInterfaces.Factories.Scenes
{
    public interface ISceneFactory
    {
        UniTask<IScene> Create(object payload);
    }
}