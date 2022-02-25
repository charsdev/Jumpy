using UnityEngine;

namespace Jumpy
{
    public class Item : MonoBehaviour
    {
        private Animator _animator;
        private bool collected;
        //public delegate void Collect();
        //public event Collect OnCollect;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update() { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") && !enabled) return;

            Collect();
            //if (OnCollect != null)
            //{
            //    OnCollect();
            //}
        }

        public void Collect()
        {
            if (!collected)
            {
                collected = true;
                UIManager.instance.Score.SetTrigger("Collect");
                _animator.SetTrigger("Collect");
                Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
                UIManager.instance.Score.Value++;
            }
        }
    }

}
