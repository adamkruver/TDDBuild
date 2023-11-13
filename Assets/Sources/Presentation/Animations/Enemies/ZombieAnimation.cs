using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sources.PresentationInterfaces.Animations.Enemies;
using UnityEngine;

namespace Sources.Presentation.Animations.Enemies
{
    public class ZombieAnimation : MonoBehaviour, IZombieAnimation
    {
        [SerializeField] private Animator _animator;

        private static readonly int s_hitHash = Animator.StringToHash("Hit");
        private static readonly int s_hitProjectionHash = Animator.StringToHash("HitProjection");
        private static readonly int s_dieHash = Animator.StringToHash("Die");

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void ResetToIdle()
        {
            _animator.applyRootMotion = false;
            _animator.Rebind();
        }

        public void Hit(float lastHitForwardProjection)
        {
            _animator.SetFloat(s_hitProjectionHash, lastHitForwardProjection);
            _animator.SetTrigger(s_hitHash);
        }

        public UniTask Fall(float lastHitForwardProjection)
        {
            _animator.applyRootMotion = true;
            _animator.SetFloat(s_hitProjectionHash, lastHitForwardProjection);
            _animator.SetBool(s_dieHash, true);
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            return UniTask.Delay(TimeSpan.FromSeconds(animationLength),
                cancellationToken: _cancellationTokenSource.Token);
        }

        public UniTask Decay()
        {
            _animator.applyRootMotion = false;
            
            return transform
                .DOMove(transform.position + Vector3.down/2, 6f)
                .AsyncWaitForCompletion()
                .AsUniTask();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            transform.DOKill();
        }
    }
}