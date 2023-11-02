using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class DoubleLaserTwiceGun : WeaponBase, IConstructable
    {
        public DoubleLaserTwiceGun(IBullet bullet, ITimeService timeService, WeaponFab weaponFab) : base(bullet, timeService, weaponFab)
        {
        }
    }
}