using System.Collections.Generic;
using Sources.Controllers.Scenes;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.Scenes;
using Sources.Infrastructure.StateMachines;
using Sources.InfrastructureInterfaces.Factories.Scenes;

namespace Sources.Infrastructure.Factories.Services
{
    public class SceneServiceFactory
    {
        public SceneService Create()
        {
            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory();
            GameplaySceneFactory gameplaySceneFactory = new GameplaySceneFactory();

            Dictionary<string, ISceneFactory> sceneStates = new Dictionary<string, ISceneFactory>()
            {
                ["MainMenu"] = mainMenuSceneFactory,
                ["Gameplay"] = gameplaySceneFactory,
            };

            StateMachine<IScene> sceneStateMachine = new StateMachine<IScene>();

            return new SceneService(sceneStateMachine, sceneStates);
        }
    }
}