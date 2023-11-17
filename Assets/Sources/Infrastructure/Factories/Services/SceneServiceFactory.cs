using System.Collections.Generic;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.Scenes;
using Sources.InfrastructureInterfaces.Factories.Scenes;

namespace Sources.Infrastructure.Factories.Services
{
    public class SceneServiceFactory
    {
        public SceneService Create()
        {
            Dictionary<string, ISceneFactory> sceneStates = new Dictionary<string, ISceneFactory>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates["MainMenu"] = new MainMenuSceneFactory(sceneService);
            sceneStates["Gameplay"] = new GameplaySceneFactory(sceneService);

            return sceneService;
        }
    }
}