using System;
using System.Collections.Generic;
using Sources.App.Di.Dependencies;

namespace Sources.App.Di.Activation
{
    public class ActivateInterfaceStrategy : IActivateStrategy
    {
        private readonly ConstructorReader _constructorReader;
        private readonly ObjectsContainer _objectsContainer;
        private readonly ActivateStrategiesFactory _activateStrategiesFactory;
        private readonly Dictionary<ObjectType, IActivateStrategy> _activationStrategies;
        private readonly ObjectActivator _objectActivator;
        private readonly DependenciesMapper _dependenciesMapper;

        public ActivateInterfaceStrategy(
            ConstructorReader constructorReader,
            ObjectsContainer objectsContainer,
            ActivateStrategiesFactory activateStrategiesFactory,
            ObjectActivator objectActivator,
            DependenciesMapper dependenciesMapper
        )
        {
            _constructorReader = constructorReader;
            _objectsContainer = objectsContainer;
            _activateStrategiesFactory = activateStrategiesFactory;
            _objectActivator = objectActivator;
            _dependenciesMapper = dependenciesMapper;
        }

        public object Activate(Type interfaceType)
        {
            if (TryGetObject(interfaceType, out object obj))
                return obj;
            
            Type type = _dependenciesMapper.Get(interfaceType);

            Type[] dependencyTypes = _constructorReader.GetParametersType(type);
            object[] dependencies = new object[dependencyTypes.Length];

            for (int i = 0; i < dependencyTypes.Length; i++)
            {
                var dependencyType = dependencyTypes[i];
                var dependency = _objectsContainer.Get(dependencyType);

                if (dependency == null)
                {
                    dependency = new ObjectFactory(dependencyType, _activateStrategiesFactory).Create();
                }

                dependencies[i] = dependency;
            }

            obj = _objectActivator.Activate(type, dependencies);

            _objectsContainer.Register(interfaceType, obj);
            return obj;
        }
        
        private bool TryGetObject(Type type, out object obj)
        {
            obj = _objectsContainer.Get(type);
            return obj != null;
        }
    }
}