using Sources.Domain.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;
using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.Controllers.Bullets
{
    public class RocketPresenter : BulletPresenter
    {
        private readonly Rocket _bullet;

        public RocketPresenter(IBulletView view, Rocket bullet) : base(view, bullet) =>
            _bullet = bullet;

        public IEnemyView Enemy => _bullet.Enemy;
    }
}