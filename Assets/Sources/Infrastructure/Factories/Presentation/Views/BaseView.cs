using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private Transform _doors;
        
        public Vector3 DoorsPosition => _doors.position;
    }
}