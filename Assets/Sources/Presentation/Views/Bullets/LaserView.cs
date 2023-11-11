﻿using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserView : BulletView, IBulletView
    {
        [SerializeField] private float _maxDistance = 100f;
        [Range(.1f, 5)] [SerializeField] private float _time = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private AnimationCurve _widthCurve;
        [Range(.01f, 5)] [SerializeField] private float _widthMultiplier = .2f;
        [SerializeField] private AnimationCurve _colorCurve;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private ParticleSystem _distortionParticleSystem;
        [SerializeField] private ParticleSystem _targetParticleSystem;
        [SerializeField] private float _distortionRate = 20f;

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
        }

        public override void Shoot()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            ShootAsync(Position, CalculateDestination(), _cancellationTokenSource.Token);
        }

        public override void OnShootTarget(IDamageable damageable, Vector3 forward)
        {
        }

        private async UniTask ShootAsync(Vector3 from, Vector3 to, CancellationToken token)
        {
            float time = 0;

            SetupVector(from, to);
            
            _distortionParticleSystem.Play();
            _targetParticleSystem.Play();
            
            while (time < 1f)
            {
                time = Mathf.MoveTowards(time, 1f, Time.deltaTime / _time);
                Evaluate(from, to, time);

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

        private void Evaluate(Vector3 from, Vector3 to, float time)
        {
            time = Mathf.Clamp01(time);
            
            Color color = Color.Lerp(_startColor, _endColor, Mathf.Clamp01(_colorCurve.Evaluate(time)));
            float width = _widthCurve.Evaluate(time) * _widthMultiplier;
            
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
        }

        private Vector3 CalculateDestination()
        {
            if (Physics.Raycast(Position, Forward, out RaycastHit hit, _maxDistance, _mask) == false)
                return Position + Forward * _maxDistance;

            if (hit.collider.TryGetComponent(out IDamageable target)) 
                Presenter.Fire(target, -hit.normal);

            return hit.point;
        }
    }
}