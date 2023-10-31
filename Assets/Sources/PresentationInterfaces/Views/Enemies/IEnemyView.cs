using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Enemies
{
    public interface IEnemyView
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
    }
}