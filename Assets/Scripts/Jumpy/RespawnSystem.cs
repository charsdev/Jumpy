using UnityEngine;

namespace Jumpy
{
    public class RespawnSystem : MonoBehaviour
    {
        public static Transform Checkpoint;
        public static Transform Player;

        private void Start()
        {
            if (!Player)
                Player = GameObject.FindGameObjectWithTag("Player").transform;

            if (!Checkpoint)
                Checkpoint = GameObject.Find("Initial Position").transform;
        }

        public static void Respawn()
        {
            Player.position = Checkpoint.position;
        }

    }
}
