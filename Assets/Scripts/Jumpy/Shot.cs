using UnityEngine;

namespace Jumpy
{
    public class Shot : MonoBehaviour
    {
        public float Speed;

        private void Update() => transform.position += transform.right * Speed * Time.deltaTime;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CollisionHandle(collision.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionHandle(collision.gameObject);
        }

        private void CollisionHandle(GameObject go)
        {
            Debug.Log(go.name);
            Destroy(gameObject);
        }

    }

}
