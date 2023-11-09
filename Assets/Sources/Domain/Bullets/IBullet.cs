﻿using Sources.Domain.HealthPoints;
using UnityEngine;

namespace Sources.Domain.Bullets
{
    public interface IBullet
    {
        float Damage { get; }

        float Speed { get; }
        void Attack(IDamageable damageable, Vector3 direction);
    }
}