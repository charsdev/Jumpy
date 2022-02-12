using UnityEngine;

namespace Chars
{
    public class Room : MonoBehaviour
    {
        public Camera Camera;
        public bool CurrentRoom = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CurrentRoom = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CurrentRoom = false;
            }
        }

        private void Update() => Camera.gameObject.SetActive(CurrentRoom);
    }
}
