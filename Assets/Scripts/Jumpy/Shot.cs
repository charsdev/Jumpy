using UnityEngine;
using Chars.Tools;

namespace Jumpy
{
    public class Shot : MonoBehaviour, IPooleable
    {
        public float Speed;
        private void Update() => Move();
        private void Move() => transform.position += transform.right * Speed * Time.deltaTime;
        private void OnTriggerEnter2D(Collider2D collision) => CollisionHandle();
        private void OnCollisionEnter2D(Collision2D collision) => CollisionHandle();

        private void CollisionHandle()
        {
            Release();
        }

        public void Release()
        {
            ObjectPool.Instance.ReturnToPool(gameObject, "Shot");
        }

        public void Capture()
        {
            Debug.Log($"Capture {gameObject.name}", this);
        }
    }

}
