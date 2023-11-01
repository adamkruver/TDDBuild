using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class MiniGun : WeaponBase, IConstructable
    {
        public MiniGun(IBullet bullet, ITimeService timeService, WeaponFab weaponFab) : base(
            bullet, timeService, weaponFab
        )
        {
        }
    }
}