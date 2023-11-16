using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserView : BulletViewBase, IBulletView
    {
        [SerializeField] private float _maxDistance = 100f;
        [Range(.1f, 5)] [SerializeField] private float _time = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private AnimationCurve _widthCurve;
        [Range(.01f, 5)] [SerializeField] private float _widthMultiplier = .2f;
        [SerializeField] private ParticleSystem _distortionParticleSystem;
        [SerializeField] private ParticleSystem _targetParticleSystem;
        [SerializeField] private float _distortionRate = 20f;
        [SerializeField] private AudioSource _shootSource;

        private LineRenderer _lineRenderer;
        private CancellationTokenSource _cancellationTokenSource;
        private Transform _transform;
        private ParticleSystem.ShapeModule _distortionShape;
        private Transform _distortionTransform;
        private ParticleSystem.EmissionModule _distortionEmission;
        private Transform _targetTransform;

        private Vector3 Position => _transform.position;
        private Vector3 Forward => _transform.forward;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _transform = GetComponent<Transform>();
            _distortionShape = _distortionParticleSystem.shape;
            _distortionTransform = _distortionParticleSystem.transform;
            _distortionEmission = _distortionParticleSystem.emission;
            _targetTransform = _targetParticleSystem.transform;
            _lineRenderer.startWidth = 0;
            _lineRenderer.endWidth = 0;
            _shootSource.pitch = Random.Range(.95f, 1.1f);
        }

        public override void Shoot()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            ShootAsync(Position, CalculateDestination(), _cancellationTokenSource.Token);
        }

        private async UniTask ShootAsync(Vector3 from, Vector3 to, CancellationToken token)
        {
            float time = 0;

            SetupVector(from, to);

            _distortionParticleSystem.Play();
            _targetParticleSystem.Play();
            _shootSource.Play();

            
            while (time < 1f)
            {
                time = Mathf.MoveTowards(time, 1f, Time.deltaTime / _time);
                Evaluate(time);

                await UniTask.Yield(token);
            }
        }

        private void SetupVector(Vector3 from, Vector3 to)
        {
            _lineRenderer.SetPositions(
                new Vector3[]
                {
                    from,
                    to
                }
            );

            Vector3 direction = to - from;

            float halfDistance = direction.magnitude / 2;

            _distortionShape.radius = halfDistance;
            _distortionTransform.localPosition = halfDistance * Vector3.forward;
            _distortionEmission.rateOverTime = halfDistance * _distortionRate;
            _targetTransform.position = to;
            _targetTransform.forward = -direction.normalized;
        }

        private void Evaluate(float time)
        {
            time = Mathf.Clamp01(time);

            float width = _widthCurve.Evaluate(time) * _widthMultiplier;

            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
        }

        private Vector3 CalculateDestination()
        {
            if (Physics.Raycast(Position, Forward, out RaycastHit hit, _maxDistance, _mask) == false)
                return Position +  _transform.rotation * new Vector3(0, 0, _maxDistance);

            if (hit.collider.TryGetComponent(out IDamageable target))
                OnShootTarget(target, -hit.normal);

            return hit.point;
        }
    }
}