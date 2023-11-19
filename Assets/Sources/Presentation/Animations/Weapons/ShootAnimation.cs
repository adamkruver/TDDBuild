using Sources.PresentationInterfaces.Animations.Weapons;
using UnityEngine;

namespace Sources.Presentation.Animations.Weapons
{
    public class ShootAnimation : MonoBehaviour, IWeaponAnimation
    {
        [SerializeField] private Animator _animator;

        private static readonly int s_shootHash = Animator.StringToHash("Shoot");

        public void Shoot() =>
            _animator.SetTrigger(s_shootHash);
    }
}