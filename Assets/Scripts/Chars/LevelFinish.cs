using UnityEngine;

namespace Chars.Tools 
{
    public class LevelFinish : MonoBehaviour
    {
        public Room room;
        public CameraFollow cameraFollow;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                room.enabled = false;
                cameraFollow.enabled = false;
                GameManager.Instance.SetGameState(GameManager.Status.GameOver);
            }
        }
    }

}
