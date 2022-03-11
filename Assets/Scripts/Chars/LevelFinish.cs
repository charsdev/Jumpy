using System;
using UnityEngine;

namespace Chars.Tools 
{
    public class LevelFinish : MonoBehaviour
    {
        public string NextLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                LevelManager.Instance.SetNextLevel(NextLevel);
                GameManager.Instance.SetGameState(GameManager.Status.GameOver);
                EventManager.TriggerEvent("GameOver", EventArgs.Empty, this);
            }
        }
    }

}
