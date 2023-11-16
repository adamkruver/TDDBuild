using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Presentation.Views.Models
{
    public class RandomModelSelectorView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            int index = Random.Range(0, _objects.Length);
            GameObject model = _objects[index];
            
            foreach (GameObject @object in _objects)
            {
                if(@object != model)
                    DestroyImmediate(@object);
            }
            
            model.SetActive(true);
            _animator.gameObject.SetActive(false);
        }

        private void Start()
        {
            _animator.gameObject.SetActive(true);
        }
    }
}