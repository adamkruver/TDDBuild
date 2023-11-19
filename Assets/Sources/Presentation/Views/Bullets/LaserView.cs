using System.Threading;
using Sources.Controllers.Projectiles;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserView : PresentationViewBase<LaserPresenter>, ILaserView, IProjectileView
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxDistance = 100f;
        [SerializeField] private float _distortionRate = 20f;
        [SerializeField] private ParticleSystem _distortionParticleSystem;
        [SerializeField] private ParticleSystem _targetParticleSystem;

        [field: Range(.1f, 5)]
        [field: SerializeField]
        public float Duration { get; private set; } = 1f;

        [field: Range(.01f, 5)]
        [field: SerializeField]
        public float NativeWidth { get; private set; } = .2f;

        [field: SerializeField] public AnimationCurve WidthCurve { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }

        private LineRenderer _lineRenderer;
        private CancellationTokenSource _cancellationTokenSource;
        private Transform _transform;
        private ParticleSystem.ShapeModule _distortionShape;
        private Transform _distortionTransform;
        private ParticleSystem.EmissionModule _distortionEmission;
        private Transform _targetTransform;

        public Vector3 Forward => _transform.forward;
        public Vector3 Position => Transform.position;

        public void Shoot() =>
            Presenter?.Shoot();

        public void SetParent(Transform parent) =>
            Transform.SetParent(parent, false);

        public void SetLaserPositions(Vector3 from, Vector3 to) =>
            _lineRenderer.SetPositions(new Vector3[] { from, to });

        public void SetLaserWidth(float width)
        {
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
        }

        public void StartBurnTarget(Vector3 position, Vector3 direction)
        {
            _targetTransform.position = position;
            _targetTransform.forward = direction;

            _targetParticleSystem.Play();
        }

        public void FinishBurnTarget() =>
            _targetParticleSystem.Stop();

        public void StartDistortion(Vector3 from, Vector3 to)
        {
            float halfDistance = (to - from).magnitude / 2;

            _distortionShape.radius = halfDistance;
            _distortionTransform.localPosition = halfDistance * Vector3.forward;
            _distortionEmission.rateOverTime = halfDistance * _distortionRate;

            _distortionParticleSystem.Play();
        }

        public void FinishDistortion() =>
            _distortionParticleSystem.Stop();

        public (Vector3 position, IDamageable target)
            Raycast(Ray ray, float maxDistance) // TODO: Move To Raycast Service
        {
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask) == false)
                return (Position + Forward * _maxDistance, null);

            if (hit.collider.TryGetComponent(out IDamageable target) == false)
                return (hit.point, null);

            return (hit.point, target);
        }

        protected override void OnAwake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _transform = GetComponent<Transform>();
            _distortionShape = _distortionParticleSystem.shape;
            _distortionTransform = _distortionParticleSystem.transform;
            _distortionEmission = _distortionParticleSystem.emission;
            _targetTransform = _targetParticleSystem.transform;
            _lineRenderer.startWidth = 0;
            _lineRenderer.endWidth = 0;
        }
    }
}