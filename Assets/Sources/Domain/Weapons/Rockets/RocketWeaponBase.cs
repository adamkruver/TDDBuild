using System.Linq;
using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;
using UnityEngine;

namespace Sources.Domain.Weapons.Rockets
{
    public abstract class RocketWeaponBase : WeaponBase, IRocketWeapon
    {
        private Vector3 _destination;

        protected RocketWeaponBase(
            int shootPoints,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints,
            timeService,
            weaponFab,
            upgradeSystem
        )
        {
        }

        public Rocket[] Rockets { get; private set; }
        public bool HasNoRockets => Rockets.Count(rocket => rocket != null) == 0;

        public void SetDestination(Vector3 position) =>
            _destination = position;

        public void SetRockets(Rocket[] rockets) =>
            Rockets = rockets;

        protected override void OnShoot()
        {
            Rockets[ShootPointIndex].SetDestination(_destination);
            Rockets[ShootPointIndex] = null;
            InvokeShootingEvent();
            SetNextShootPoint();
        }
    }
}