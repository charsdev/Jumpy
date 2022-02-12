using UnityEngine;

namespace Jumpy
{
    public class EchoEffect : MonoBehaviour
    {
        private float timeBeetweenSpawns;
        public float startTimeBeetweenSpawns;
        public float timeBeetweenDestroy = 8f;
        public GameObject prefab;
        public CharacterBody CharacterBody;

        private void Update()
        {
            if (timeBeetweenSpawns <= 0)
            {
                GameObject instance = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
                Destroy(instance, timeBeetweenDestroy);
                timeBeetweenSpawns = startTimeBeetweenSpawns;
            }
            else
            {
                timeBeetweenSpawns -= Time.deltaTime;
            }

            //TODO: Disengage
            enabled = CharacterBody.OnGround && enabled;
        }
    }
}
