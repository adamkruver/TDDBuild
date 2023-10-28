using Sources.App.Core;
using Sources.Infrastructure.Factories.Services;
using Sources.Infrastructure.Services.Scenes;
using Sources.Infrastructure.Services.Scenes.Events;
using Sources.Infrastructure.Services.Scenes.Loaders;
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

            SceneService sceneService = new SceneServiceFactory().Create();

            SceneLoaderService sceneLoaderService = new SceneLoaderService();

            CurtainView curtainView = Object.Instantiate(Resources.Load<CurtainView>("Views/Bootstrap/CurtainView"));

            sceneService.AddEventListener(new BeforeExitSceneEventHandler(sceneName => curtainView.Show()));
            sceneService.AddEventListener(new AfterExitSceneEventHandler(sceneLoaderService.Load));
            sceneService.AddEventListener(new AfterEnterSceneEventHandler(sceneName => curtainView.Hide()));

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}