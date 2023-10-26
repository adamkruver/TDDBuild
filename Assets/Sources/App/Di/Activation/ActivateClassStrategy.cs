using System;
using System.Collections.Generic;

namespace Sources.App.Di.Activation
{
    public class ActivateClassStrategy : IActivateStrategy
    {
        private readonly ConstructorReader _constructorReader;
        private readonly ObjectsContainer _objectsContainer;
        private readonly ActivateStrategiesFactory _activateStrategiesFactory;
        private readonly Dictionary<ObjectType, IActivateStrategy> _activationStrategies;
        private readonly ObjectActivator _objectActivator;

        public ActivateClassStrategy(
            ConstructorReader constructorReader,
            ObjectsContainer objectsContainer,
            ActivateStrategiesFactory activateStrategiesFactory,
            ObjectActivator objectActivator
        )
        {
            _constructorReader = constructorReader;
            _objectsContainer = objectsContainer;
            _activateStrategiesFactory = activateStrategiesFactory;
            _objectActivator = objectActivator;
        }

        public object Activate(Type type)
        {
            if (TryGetObject(type, out object obj))
                return obj;
            
            Type[] dependencyTypes = _constructorReader.GetParametersType(type);
            object[] dependencies = new object[dependencyTypes.Length];

            for (int i = 0; i < dependencyTypes.Length; i++)
            {
                var dependencyType = dependencyTypes[i];
                object dependency = null;

                if (dependencyType.IsInterface)
                    dependency = _objectsContainer.Get(dependencyType);

                dependency ??= new ObjectFactory(dependencyType, _activateStrategiesFactory).Create();

                dependencies[i] = dependency;
            }

            return _objectActivator.Activate(type, dependencies);
        }

        private bool TryGetObject(Type type, out object obj)
        {
            obj = _objectsContainer.Get(type);
            return obj != null;
        }
    }
}