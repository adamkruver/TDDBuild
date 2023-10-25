using Sources.Controllers.Scenes;
using Sources.InfrastructureInterfaces.Factories.Scenes;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GameplaySceneFactory : ISceneFactory
    {
        public IScene Create(object payload) => 
            new GameplayScene();
    }
}