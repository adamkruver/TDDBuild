using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.App.Core;
using Sources.Controllers.Scenes;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.SceneLoaders;
using Sources.Infrastructure.StateMachines;
using Sources.InfrastructureInterfaces.StateMachines;
using Sources.Presentation.Views.Bootstrap;
using UnityEngine;

namespace Sources.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore))
                .AddComponent<AppCore>();

            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory();
            GameplaySceneFactory gameplaySceneFactory = new GameplaySceneFactory();
            
            Dictionary<string, IState> sceneStates = new Dictionary<string, IState>()
            {
                ["MainMenu"] = new SceneState(mainMenuSceneFactory),
                ["Gameplay"] = new SceneState(gameplaySceneFactory),
            };

            StateMachine sceneStateMachine = new StateMachine(sceneStates);

            CurtainView curtainView = Object.Instantiate(Resources.Load<CurtainView>("Views/Bootstrap/CurtainView"));
            
            sceneStateMachine.AddEnterHandler(sceneName => curtainView.Show());
            sceneStateMachine.AddEnterHandler(sceneName => new SceneLoaderService().Load(sceneName));
            sceneStateMachine.AddExitHandler(() => UniTask.CompletedTask);
            sceneStateMachine.AddExitHandler(() => UniTask.Delay(2000));
            sceneStateMachine.AddExitHandler(curtainView.Hide);
            
            appCore.Construct(sceneStateMachine);

            return appCore;
        }
    }
}