using UnityEngine;

namespace Chars
{
    public class EchoEffect : MonoBehaviour
    {
        private float _timeBeetweenSpawns;
        public float startTimeBeetweenSpawns;
        public float timeBeetweenDestroy = 8f;
        public GameObject prefab;
        public CharacterBody CharacterBody;

        private void Update()
        {
            if (_timeBeetweenSpawns <= 0)
            {
                GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
                Destroy(instance, timeBeetweenDestroy);
                _timeBeetweenSpawns = startTimeBeetweenSpawns;
            }
            else
            {
                _timeBeetweenSpawns -= Time.deltaTime;
            }

        }
    }
}
