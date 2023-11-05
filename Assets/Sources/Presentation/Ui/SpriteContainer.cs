using UnityEngine;

namespace Sources.Presentation.Ui
{
    public class SpriteContainer : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        
        public Sprite GetByIndex(int index)
        {
            if (index < 0 || index >= _sprites.Length)
                return null;
            
            return _sprites[index];
        }

        public Sprite GetByIndexOrLast(int index) => 
            GetByIndex(index) ?? GetByIndex(_sprites.Length - 1);
    }
}