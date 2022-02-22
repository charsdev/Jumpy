using Chars.Tools;
using UnityEngine;

namespace Jumpy
{
    public abstract class EnemyCharacter : MonoBehaviour, IPooleable, IKilleable
    {
        public Transform Target;
        public float Speed;
        protected Rigidbody2D _rigidbody2D;
        protected SpriteRenderer _spriteRenderer;
        protected Transform Model;

        protected virtual void CharacterLogic()
        {
            //override this for unique character Logic
        }

        public virtual void Capture()
        {
            ObjectPool.Instance.CaptureFromPool(gameObject, gameObject.name);
        }

        public virtual void Release()
        {
            ObjectPool.Instance.ReturnToPool(gameObject, gameObject.name);
        }

        public virtual void Die()
        {
            Release();
        }

        protected virtual void Start()
        {
            Target = GameManager.Instance.player.transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            CharacterLogic();
        }
       
    }
}