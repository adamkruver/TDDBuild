using Sources.Controllers.Constructions;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Constructions;
using UnityEngine;

namespace Sources.Presentation.Previews.Constructions
{
    public class TurretConstructionPreview : PresentationViewBase<TurretConstructionPresenter>, ITurretConstructionView
    {
        [SerializeField] private WeaponAttackRadiusView _attackRadiusView;

        public void SetPosition(Vector3 worldPosition) =>
            Transform.position = worldPosition;

        public void SetAttackRadius(float radius) =>
            _attackRadiusView.SetRadius(radius);

        public void Build(Vector3 worldPosition) =>
            Presenter.Build(worldPosition);
    }
}