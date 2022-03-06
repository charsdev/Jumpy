using System;
using UnityEngine;

namespace Jumpy
{
    public class AddScoreParams : EventArgs 
    {
        public int Value { get; set; }
        public AddScoreParams(int value)
        {
            Value = value;
        }
    }

    public class Item : MonoBehaviour
    {
        private Animator _animator;
        private bool collected;
        public SpriteRenderer spriteRenderer;
        public ItemData itemData;

        private void Start()
        {
            spriteRenderer.sprite = itemData.Sprite;
            name = itemData.Name;
           _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") && !enabled) return;
            Collect();
        }

        public void Collect()
        {
            if (!collected)
            {
                collected = true;
                _animator.SetTrigger("Collect");
                Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
                EventManager.TriggerEvent("AddScore", new AddScoreParams(1), this);
            }
        }
    }

}
