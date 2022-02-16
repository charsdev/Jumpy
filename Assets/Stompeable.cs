using Jumpy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompeable : MonoBehaviour
{

    [SerializeField] private bool _useParent;
    [SerializeField] private GameObject objectToStomp;
    private Jumpy.IKilleable killeable;

    private void Start()
    {
        if (_useParent)
            objectToStomp = transform.parent.gameObject;

        killeable = objectToStomp.GetComponent<Jumpy.IKilleable>();   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            killeable.Die();

            if (other.gameObject.TryGetComponent(out CharacterBody characterBody)) 
            {
                characterBody.Rigidbody2D.velocity = Vector2.zero; //force velocity 0;
                characterBody.Rigidbody2D.velocity += (Vector2)characterBody.Up * 30f;
            }
        }
    }
}
