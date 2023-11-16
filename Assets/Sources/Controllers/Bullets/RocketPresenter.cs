using Sources.Domain.Bullets;
using Sources.Presentation.Views.Bullets;

namespace Sources.Controllers.Bullets
{
    public class RocketPresenter : BulletPresenter
    {
        private readonly RocketView _view;
        private readonly Rocket _bullet;

        public RocketPresenter(RocketView view, Rocket bullet) : base(view, bullet)
        {
            _view = view;
            _bullet = bullet;
        }

        public override void Enable()
        {
            _bullet.EnemyChanged += OnEnemyChanged;
            base.Enable();
        }

        private void OnEnemyChanged() =>
            _view.SetEnemyPosition(_bullet.Enemy.Position);

        public override void Disable()
        {
            base.Disable();
            _bullet.EnemyChanged -= OnEnemyChanged;
        }
    }
}