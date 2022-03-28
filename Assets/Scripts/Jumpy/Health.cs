using Jumpy;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _death;
    public ParticleSystem DeathParticle;
    private JumpyAnimator _jumpyAnimator;

    public void Start()
    {
        DeathParticle.Stop();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumpyAnimator = GetComponent<JumpyAnimator>();
    }

    public void Die()
    {
        if (!_death)
        {
            _death = true;
            StartCoroutine(DieCoroutine());
        }
    }

    private IEnumerator DieCoroutine()
    {
        _jumpyAnimator.TriggerPlayerHit();
        GameManager.Instance.SetInput(false);
        yield return new WaitForSeconds(0.5f);
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _jumpyAnimator.SetSpriteRenderer(false);
        DeathParticle.Play();
        EventManager.TriggerEvent("FadeIn", new FadeEventParams(1f), this);
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.SpawnPlayer();
        Reset();
        yield return null;
    }


    public void DoknockBack(Vector2 force)
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

    private void Reset()
    {
        EventManager.TriggerEvent("FadeOut", new FadeEventParams(1f), this);
        _death = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _jumpyAnimator.SetSpriteRenderer(true);
        GameManager.Instance.SetInput(true);
    }
}
