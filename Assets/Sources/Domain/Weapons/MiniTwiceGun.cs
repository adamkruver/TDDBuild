using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class MiniTwiceGun : WeaponBase, IConstructable
    {
        public MiniTwiceGun(IBullet bullet, ITimeService timeService, WeaponFab weaponFab) : base(
            bullet, timeService, weaponFab
        )
        {
        }
    }
}