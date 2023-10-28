using UnityEngine;

namespace Sources.Presentation.Ui
{
    public class ContainerUi : MonoBehaviour
    {
        private Transform _transform;

        private void Awake() => 
            _transform = GetComponent<Transform>();

        public void AddChild(GameObject child) =>
            child.transform.SetParent(_transform, false);
    }
}