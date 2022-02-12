using UnityEngine;

namespace Jumpy
{
    public class CheckPoint : MonoBehaviour
    {
        public Transform respawnPosition;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //TODO: Disengage
                RespawnSystem.Checkpoint = respawnPosition;
            }
        }
    }
}

