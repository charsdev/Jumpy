using UnityEngine;
using Chars.Tools;

namespace Jumpy
{
    public class EnemyFollow : MonoBehaviour, IPooleable, IKilleable
    {
        public Transform Target;
        public float Speed;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        public void Capture()
        {
            ObjectPool.Instance.CaptureFromPool(gameObject, "EnemyFollow");
        }

        public void Release()
        {
            ObjectPool.Instance.ReturnToPool(gameObject, "EnemyFollow");
        }

        public void Die()
        {
            Release();
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Target = GameObject.FindGameObjectWithTag("Player").transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 delta = Target.transform.position - transform.position;
            float deltaX = delta.normalized.x;
            RaycastHit2D hit = DebugTool.RaycastHit(transform.position, deltaX * Vector2.right, 5, LayerMask.GetMask("Player"), Color.red, true);
          
            if (hit)
            {
                _rigidbody2D.velocity = Vector2.MoveTowards(_rigidbody2D.velocity, new Vector2(deltaX * Speed, _rigidbody2D.velocity.y), Speed * Time.deltaTime);
            }
         
            _spriteRenderer.flipX = Target.transform.position.x < transform.position.x;
        }
    }
}