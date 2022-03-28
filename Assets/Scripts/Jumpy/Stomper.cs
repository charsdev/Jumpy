using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jumpy;
using Chars;
using Chars.Tools;

[RequireComponent(typeof(BoxCollider2D))]
public class Stomper : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CharacterBody _characterBody;

    private void Start() {}
    private void Update() {}

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (!enabled) return;
       if ((_layerMask.value & (1 << other.gameObject.layer)) <= 0) return;

        if (other.TryGetComponent(out IKilleable killeable))
        {
            killeable.Die();

            _characterBody.Rigidbody2D.velocity = Vector2.zero; //force velocity 0;
            _characterBody.Rigidbody2D.velocity += (Vector2)_characterBody.Up * 30f;
        }

        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossHealth>().TakeDamage(100);
        }
    }

}
