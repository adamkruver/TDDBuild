using Sources.Domain.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Controllers.Bullets
{
    public class BulletPresenter : PresenterBase
    {
        private readonly IBulletView _view;
        private readonly IBullet _bullet;

        public BulletPresenter(IBulletView view, IBullet bullet)
        {
            _view = view;
            _bullet = bullet;
        }

        public void Fire(IDamageable damageable)
        {
            _bullet.Attack(damageable);
        }
    }
}