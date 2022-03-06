using System;
using UnityEngine;

namespace Chars.Tools 
{
    public class LevelFinish : MonoBehaviour
    {
        public Room Room;
        public CameraFollow CameraFollow;
        public string NextLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Room.enabled = false;
                CameraFollow.enabled = false;
                LevelManager.Instance.SetNextLevel(NextLevel);
                GameManager.Instance.SetGameState(GameManager.Status.GameOver);
                EventManager.TriggerEvent("GameOver", EventArgs.Empty, this);
            }
        }
    }

}
