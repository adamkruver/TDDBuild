using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.PresentationInterfaces.Animations.Enemies;
using UnityEngine;

namespace Sources.Presentation.Animations.Enemies
{
    public class ZombieAnimation : MonoBehaviour, IZombieAnimation
    {
        [SerializeField] private Animator _animator;

        private static readonly int s_hitHash = Animator.StringToHash("Hit");
        private static readonly int s_dieHash = Animator.StringToHash("Die");

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void ResetToIdle() => 
            _animator.Rebind();

        public void Hit() =>
            _animator.SetTrigger(s_hitHash);

        public UniTask Die()
        {
            _animator.SetTrigger(s_dieHash);
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            return UniTask.Delay(TimeSpan.FromSeconds(animationLength),
                cancellationToken: _cancellationTokenSource.Token);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}