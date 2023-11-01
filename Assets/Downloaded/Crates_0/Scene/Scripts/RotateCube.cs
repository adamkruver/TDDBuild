using UnityEngine;

namespace Downloaded.Crates_0.Scene.Scripts
{
	public class RotateCube : MonoBehaviour {

		public float speed = 10f;
	
		// Update is called once per frame
		void Update () {
			transform.Rotate(0, speed * Time.deltaTime, 0);
	
		}
		public void AdjustSpeed(float newSpeed) {
			speed = newSpeed;
		}

	}
}
