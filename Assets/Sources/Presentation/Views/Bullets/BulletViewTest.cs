using System.Collections;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class BulletViewTest : MonoBehaviour
    {
        [SerializeField] private BulletView _bulletView;
        [SerializeField] private float _fireDelay = 1f;

        private IEnumerator Start()
        {
            while (true)
            {
                _bulletView.Shoot();

                yield return new WaitForSeconds(1f);
            }
        }
    }
}