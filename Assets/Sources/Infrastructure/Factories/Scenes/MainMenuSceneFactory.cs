using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        public async UniTask<IScene> Create(object payload)
        {
            Debug.Log("Scene is Creating");

            return new MainMenuScene();
        }
    }
}