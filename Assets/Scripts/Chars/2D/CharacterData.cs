using UnityEngine;

namespace Chars
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Chars/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject 
    {       
        public Sprite sprite;
        public string Name;
        public float Speed;
        public string Tag;
        public LayerMask layer;
    }
}
