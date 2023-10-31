using Sources.Controllers;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.Presentation.Views.Enemies
{
    public class EnemyView<T> : PresentationViewBase<T>, IEnemyView where T : PresenterBase
    {
        public Vector3 Position => Transform.position;
        public Vector3 Forward => Transform.forward;
    }
}