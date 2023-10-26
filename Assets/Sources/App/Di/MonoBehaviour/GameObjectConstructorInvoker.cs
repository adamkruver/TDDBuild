using System;
using System.Reflection;
using Sources.App.Di.Activation;

namespace Sources.App.Di.MonoBehaviour
{
    public class GameObjectConstructorInvoker
    {
        private const string Constructor = "Construct";
        private readonly UnityEngine.MonoBehaviour _monoBehaviour;
        private readonly ObjectsContainer _objectsContainer;
        private readonly ActivateStrategiesFactory _activateStrategiesFactory;

        public GameObjectConstructorInvoker(
            UnityEngine.MonoBehaviour monoBehaviour,
            ObjectsContainer objectsContainer,
            ActivateStrategiesFactory activateStrategiesFactory
        )
        {
            _monoBehaviour = monoBehaviour;
            _objectsContainer = objectsContainer;
            _activateStrategiesFactory = activateStrategiesFactory;
        }

        public UnityEngine.MonoBehaviour Invoke()
        {
            MethodInfo constructor = _monoBehaviour.GetType()
                .GetMethod(Constructor, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (constructor == null)
                return _monoBehaviour;

            ParameterInfo[] parameters = constructor.GetParameters();
            object[] dependencies = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Type type = parameters[i].ParameterType;
                object dependency = _objectsContainer.Get(type);

                dependency ??= new ObjectFactory(type, _activateStrategiesFactory).Create();

                dependencies[i] = dependency ?? throw new NullReferenceException(
                    $"{GetType()}: not found dependency for {type}");
            }

            constructor.Invoke(_monoBehaviour, dependencies);

            return _monoBehaviour;
        }
    }
}