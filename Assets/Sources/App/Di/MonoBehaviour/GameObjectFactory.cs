using System;
using System.Reflection;
using Sources.App.Di.Activation;
using UnityEngine;

namespace Sources.App.Di.MonoBehaviour
{
    public class GameObjectFactory<TObject, TConfig>
        where TObject : UnityEngine.MonoBehaviour where TConfig : IPrefabProvider, new()
    {
        private const string Constructor = "Construct";
        private readonly ObjectsContainer _objectsContainer;
        private readonly ActivateStrategiesFactory _activateStrategiesFactory;
        private readonly GameObjectResource<TObject, TConfig> _resource = new GameObjectResource<TObject, TConfig>();

        public GameObjectFactory(
            ObjectsContainer objectsContainer,
            ActivateStrategiesFactory activateStrategiesFactory
        )
        {
            _objectsContainer = objectsContainer;
            _activateStrategiesFactory = activateStrategiesFactory;
        }

        public TObject Create()
        {
            TObject gameObject = GameObject.Instantiate(_resource.Create());

            if (gameObject == null)
                throw new NullReferenceException($"{GetType()}: {typeof(TObject)} is null");

            InvokeConstructor(gameObject);

            var childs = gameObject.GetComponentsInChildren<UnityEngine.MonoBehaviour>();
            
            foreach (var child in childs)
                InvokeConstructor(child);

            return gameObject;
        }

        private void InvokeConstructor(UnityEngine.MonoBehaviour monoBehaviour)
        {
            MethodInfo constructor = monoBehaviour.GetType()
                .GetMethod(Constructor, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (constructor == null)
                return;

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

            constructor.Invoke(monoBehaviour, dependencies);
        }
    }
}