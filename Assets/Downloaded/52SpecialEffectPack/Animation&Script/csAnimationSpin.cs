using UnityEngine;

namespace Downloaded._52SpecialEffectPack.Animation_Script
{
	public class csAnimationSpin : MonoBehaviour {

		Animation an;

		void Update () {
			an = gameObject.GetComponent<Animation>();
			an.Play();
		}
	}
}
