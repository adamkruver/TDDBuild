using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class RocketView : BulletViewBase, IBulletView
    {
        [SerializeField] private float _flyHeight;
        [SerializeField] private float _flyDuration;
        [SerializeField] private AnimationCurve _heightCurve;
        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField] private ParticleSystem _fireParticleSystem;
        [SerializeField] private ParticleSystem _explosionParticleSystem;

        private CancellationTokenSource _cancellationTokenSource;
        private Vector3 _enemyPosition;

        public void SetEnemyPosition(Vector3 position)
        {
            _enemyPosition = position;
        }

        public override void Shoot()
        {
            StopFire();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            FlyAsync(_cancellationTokenSource.Token);
        }

        private async UniTask FlyAsync(CancellationToken cancellationToken)
        {
            Transform.parent = null;
            Vector3 startPosition = Transform.position;
            Vector3 endPosition = _enemyPosition;
            endPosition.y = 0;
            
            Vector3 flyDirection = endPosition - startPosition;
            Vector3 lastPosition = startPosition;

            float progress = 0;
            
            RunFire();

            while (progress < 1)
            {
                progress += Time.deltaTime / _flyDuration * (1 + _speedCurve.Evaluate(progress));

                progress = Mathf.Clamp01(progress);
                
                Vector3 position = startPosition + flyDirection * progress + Vector3.up * _flyHeight * _heightCurve.Evaluate(progress);
                Transform.position = position;
                Transform.forward = (position - lastPosition).normalized;
                lastPosition = position;
                
                await UniTask.Yield(cancellationToken);
            }
            
            StopFire();
            OnReachTarget();
        }

        private void RunFire()
        {
            _fireParticleSystem.Play();
        }

        private void StopFire()
        {
            _fireParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void OnReachTarget()
        {
            print("OnReachTarget");
            _explosionParticleSystem.Play();
        }
    }
}