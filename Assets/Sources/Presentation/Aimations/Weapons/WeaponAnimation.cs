using Sources.PresentationInterfaces.Animations.Weapons;
using UnityEngine;

namespace Sources.Presentation.Aimations.Weapons
{
    public class WeaponAnimation : IWeaponAnimation
    {
        [SerializeField] private Animator _animator;

        private static readonly int s_shootHash = Animator.StringToHash("Shoot");

        public void Fire() =>
            _animator.SetTrigger(s_shootHash);
    }
}