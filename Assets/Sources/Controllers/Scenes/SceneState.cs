using System;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Controllers.Scenes
{
    public class SceneState : IState
    {
        private readonly ISceneFactory _sceneFactory;
        private IScene _scene;

        public SceneState(ISceneFactory sceneFactory) => 
            _sceneFactory = sceneFactory ?? throw new ArgumentNullException(nameof(sceneFactory));

        public void Update(float deltaTime) =>
            _scene?.Update(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _scene?.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            _scene?.UpdateLate(deltaTime);

        public void Enter(object payload)
        {
            _scene = _sceneFactory.Create(payload) ?? throw new NullReferenceException(nameof(_scene));
            _scene.Enter(payload);
        }

        public void Exit() =>
            _scene.Exit();
    }
}