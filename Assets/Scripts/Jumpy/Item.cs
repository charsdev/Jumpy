using UnityEngine;

namespace Jumpy
{
    public class Item : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //if (JInput.JumpDown)
            //{
            //    UIManager.instance.Score.SetTrigger("Collect");
            //}
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {

                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Collect"))
                {
                    UIManager.instance.Score.SetTrigger("Collect");
                    _animator.SetTrigger("Collect");
                     Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
                    UIManager.instance.Score.Value++;
                }

            }
        }
    }

}
