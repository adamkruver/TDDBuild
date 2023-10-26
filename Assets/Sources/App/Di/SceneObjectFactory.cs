using Sources.App.Di.Activation;
using Sources.App.Di.MonoBehaviour;
using UnityEngine;

namespace Sources.App.Di
{
    public class SceneObjectFactory
    {
        private readonly ObjectsContainer _objectsContainer;
        private readonly ActivateStrategiesFactory _activateStrategiesFactory;

        public SceneObjectFactory(
            ObjectsContainer objectsContainer,
            ActivateStrategiesFactory activateStrategiesFactory)
        {
            _objectsContainer = objectsContainer;
            _activateStrategiesFactory = activateStrategiesFactory;
        }

        public void Create()
        {
            var monoBehaviours = GameObject.FindObjectsOfType<UnityEngine.MonoBehaviour>();

            foreach (var monoBehaviour in monoBehaviours)
            {
                new GameObjectConstructorInvoker(
                    monoBehaviour,
                    _objectsContainer,
                    _activateStrategiesFactory
                ).Invoke();
            }
        }
    }
}