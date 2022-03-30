using UnityEngine;

namespace Jumpy
{
    public class KillOnTouch : MonoBehaviour
    {
        public Vector2 DamageCausedKnockbackForce = new Vector2(0, 30f);

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out Health health) && enabled)
            {
                Debug.Log("hey");
                health.Die();
                var knockbackforce = DamageCausedKnockbackForce;
                Vector2 relativePosition = other.transform.position - transform.position;
                knockbackforce.x *= Mathf.Sign(relativePosition.x);
                health.DoknockBack(knockbackforce);
            }
        }
    }
}

