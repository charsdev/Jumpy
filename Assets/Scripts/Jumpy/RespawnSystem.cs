using Chars.Tools;
using UnityEngine;

namespace Jumpy
{
    //CHANGE TO EVENTS
    public class RespawnSystem : Singleton<RespawnSystem>
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

        public void Respawn()
        {
            Player.position = Checkpoint.position;
        }

    }
}
