using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services.Scenes;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly ISceneChangeService _sceneChangeService;

        public MainMenuSceneFactory(ISceneChangeService sceneChangeService) => 
            _sceneChangeService = sceneChangeService;

        public async UniTask<IScene> Create(object payload)
        {
            Debug.Log("Scene is Creating");

            return new MainMenuScene();
        }
    }
}