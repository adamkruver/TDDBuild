using UnityEngine;

namespace Sources.Presentation.Uis
{
    public class FooterUi : MonoBehaviour
    {
        private Transform _transform;

        private void Awake() => 
            _transform = GetComponent<Transform>();

        public void AddChild(GameObject child) =>
            child.transform.SetParent(_transform, false);
    }
}