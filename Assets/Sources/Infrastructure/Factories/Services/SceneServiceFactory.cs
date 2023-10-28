using System.Collections.Generic;
using Sources.Controllers.Scenes;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.Scenes;
using Sources.Infrastructure.StateMachines;

namespace Sources.Infrastructure.Factories.Services
{
    public class SceneServiceFactory
    {
        public SceneService Create()
        {
            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory();
            GameplaySceneFactory gameplaySceneFactory = new GameplaySceneFactory();

            Dictionary<string, SceneState> sceneStates = new Dictionary<string, SceneState>()
            {
                ["MainMenu"] = new SceneState(mainMenuSceneFactory),
                ["Gameplay"] = new SceneState(gameplaySceneFactory),
            };

            StateMachine<SceneState> sceneStateMachine = new StateMachine<SceneState>();

            return new SceneService(sceneStateMachine, sceneStates);
        }
    }
}