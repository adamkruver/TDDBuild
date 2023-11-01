using UnityEngine;

namespace Downloaded._52SpecialEffectPack.Animation_Script
{
	public class csParticleMove : MonoBehaviour
	{
		public float speed = 0.1f;

		void Update () {
			transform.Translate(Vector3.back * speed);
		}
	}
}
