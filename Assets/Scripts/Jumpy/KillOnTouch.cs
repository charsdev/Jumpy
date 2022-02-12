using UnityEngine;

namespace Jumpy
{
    public class KillOnTouch : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out Health health))
            {
                health.Die();
            }
        }
    }
}

