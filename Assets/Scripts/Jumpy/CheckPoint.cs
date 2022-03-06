using UnityEngine;
using Chars;

namespace Jumpy
{
    public class CheckPoint : MonoBehaviour
    {
        public Transform respawnPosition;
        protected bool _reached = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_reached) return;
                if (LevelManager.Instance == null) return;
                LevelManager.Instance.SetCurrentCheckpoint(this);
            }
        }

        public void Spawn(GameObject character)
        {
            character.transform.position = transform.position;
        }
    }
}

