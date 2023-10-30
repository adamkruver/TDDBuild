using Sources.Domain.Tiles;
using Sources.Domain.Turrets;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Repositories;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Domain.Turrets
{
    public class TurretFactory
    {
        private readonly TileRepository _tileRepository;

        public TurretFactory(TileRepository tileRepository) =>
            _tileRepository = tileRepository;

        public Turret Create(IWeapon weapon, Vector2Int position)
        {
            Turret turret = new Turret(weapon);
            TileModel tile = new TileModel(position.x, position.y, turret);
            _tileRepository.Set(tile);

            return turret;
        }
    }
}