using UnityEngine;

namespace Jumpy
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Chars/ItemData", order = 1)]
    public class ItemData : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
      
    }
}