using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class RocketViewTest : MonoBehaviour
    {
        [SerializeField] private float _fireDelay = 1f;
        [SerializeField] private Transform _enemy;

        private Queue<RocketView> _rocketViews;
        private Queue<Vector3> _startPositions;
        private Queue<Vector3> _startDirections;

        private void Awake()
        {
            RocketView[] rocketViews = GetComponentsInChildren<RocketView>() ??
                                       throw new NullReferenceException(nameof(RocketView));
            
            _startPositions = new Queue<Vector3>(rocketViews.Select(view => view.transform.position));
            _startDirections = new Queue<Vector3>(rocketViews.Select(view => view.transform.forward));
            
            _rocketViews = new Queue<RocketView>(rocketViews);
        }


        private IEnumerator Start()
        {
            while (true)
            {
                RocketView rocketView = _rocketViews.Dequeue();
                _rocketViews.Enqueue(rocketView);
                Vector3 rocketPosition = _startPositions.Dequeue();
                _startPositions.Enqueue(rocketPosition);
                Vector3 rocketDirection = _startDirections.Dequeue();
                _startDirections.Enqueue(rocketDirection);
                
                rocketView.transform.position = rocketPosition;
                rocketView.transform.forward = rocketDirection;
                rocketView.SetEnemyPosition(_enemy.position);
                rocketView.Shoot();

                yield return new WaitForSeconds(_fireDelay);
            }
        }
    }
}