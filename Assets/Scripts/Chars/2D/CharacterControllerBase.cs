using UnityEngine;

namespace Chars
{
    //This is most a class who package all the necessary for a character
    public abstract class CharacterControllerBase : MonoBehaviour
    {
        [SerializeField] protected CharacterData CharacterData; //Config
        [SerializeField] protected CharacterBody CharacterBody; //Physics handle
        [SerializeField] protected SpriteRenderer CharacterRenderer; //Change This for new HandlerClass
        [SerializeField] protected Transform CharacterModel;
        protected bool HasCharacterBody;
        [SerializeField] protected bool FacingRight;

        protected virtual void Awake()
        {
            HasCharacterBody = TryGetComponent(out CharacterBody);
            InitializeCharacter();
        }

        protected virtual void Start() { }
        protected virtual void FixedUpdate() { }
        protected virtual void Update() { }
        protected virtual void LateUpdate() { }

        protected virtual void Flip(bool flipSprite) 
        {
            if (flipSprite) 
            {
                CharacterRenderer.flipX = !CharacterRenderer.flipX;
            }    
            else
            {
                CharacterModel.localScale = Vector3.Scale(CharacterModel.localScale, new Vector3(-1, 1, 1));
            }

            FacingRight = !FacingRight;
        }

        protected virtual void InitializeCharacter()
        {
            if (CharacterData == null) return;

            if (CharacterRenderer != null)
                CharacterRenderer.sprite = CharacterData.sprite;

            name = CharacterData.name;
            //tag. = CharacterData.Tag;
            //gameObject.layer = CharacterData.layer;
        }

    }
}
