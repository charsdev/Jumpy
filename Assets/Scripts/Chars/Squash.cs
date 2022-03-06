using UnityEngine;

namespace Chars
{
    public class Squash : MonoBehaviour
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void SquashScale(float value)
        {
            if (value == 0f)
                return;

            transform.localScale = new Vector3(1 / value, value, 1 / value);
        }
    }
}