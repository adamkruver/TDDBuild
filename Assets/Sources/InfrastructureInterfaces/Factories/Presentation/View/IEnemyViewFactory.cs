using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.View
{
    public interface IEnemyViewFactory
    {
        public IEnemyView Create(string type, Vector3 spawnPosition);
    }
}