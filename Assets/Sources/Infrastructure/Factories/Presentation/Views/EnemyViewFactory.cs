using System;
using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class EnemyViewFactory : IEnemyViewFactory
    {
        private readonly Dictionary<string, Func<Vector3, IEnemyView>> _viewFactories;

        public EnemyViewFactory(Dictionary<string, Func<Vector3, IEnemyView>> viewFactories) => 
            _viewFactories = viewFactories 
                             ?? throw new ArgumentNullException(nameof(viewFactories));

        public IEnemyView Create(string type, Vector3 spawnPosition)
        {
            if (_viewFactories.ContainsKey(type) == false)
                throw new KeyNotFoundException(type);

            return _viewFactories[type](spawnPosition);
        }
    }
}