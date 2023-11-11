using System.Collections;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class LaserViewTest : MonoBehaviour
    {
        [SerializeField] private LaserView _laserView;
        [SerializeField] private float _fireDelay = 1f;

        private IEnumerator Start()
        {
            while (true)
            {
                _laserView.Shoot();

                yield return new WaitForSeconds(_fireDelay);
            }
        }
    }
}