using System;
using System.Collections.Generic;
using Sources.App.Di.Dependencies;

namespace Sources.App.Di.Activation
{
    public class ActivateStrategiesFactory
    {
        private readonly Dictionary<ObjectType, IActivateStrategy> _strategies =
            new Dictionary<ObjectType, IActivateStrategy>();

        private readonly ConstructorReader _constructorReader = new ConstructorReader();
        private readonly DependenciesMapper _dependenciesMapper = new DependenciesMapper();
        private readonly ObjectActivator _objectActivator = new ObjectActivator();

        private readonly ObjectsContainer _objectsContainer;

        public ActivateStrategiesFactory(ObjectsContainer objectsContainer)
        {
            _objectsContainer = objectsContainer;
            AddStrategies();
        }

        public IActivateStrategy Get(ObjectType objectType)
        {
            if (_strategies.ContainsKey(objectType) == false)
                throw new NullReferenceException($"{GetType()}: not found activate strategy for {objectType}");

            return _strategies[objectType];
        }

        private void AddStrategies()
        {
            _strategies.Add(
                ObjectType.Class,
                new ActivateClassStrategy(
                    _constructorReader,
                    _objectsContainer,
                    this,
                    _objectActivator
                )
            );

            _strategies.Add(
                ObjectType.Interface,
                new ActivateInterfaceStrategy(
                    _constructorReader,
                    _objectsContainer,
                    this,
                    _objectActivator,
                    _dependenciesMapper
                )
            );
        }
    }
}