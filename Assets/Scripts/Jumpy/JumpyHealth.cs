using Jumpy;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class JumpyHealth : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _death;
    private JumpyController _jumpyController;
    private JumpyAnimator _jumpyAnimator;

    [HideInInspector]
    public UnityEvent OnHit;

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumpyController = GetComponent<JumpyController>();
        _jumpyAnimator = GetComponent<JumpyAnimator>();
    }

    public void Die()
    {
        if (!_death)
        {
            _death = true;
            StopAllCoroutines();
            StartCoroutine(DieCoroutine());
        }
    }

    private IEnumerator DieCoroutine()
    {
        OnHit.Invoke();
   
        yield return new WaitForSeconds(0.25f);

        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.velocity = Vector2.zero;
        GameManager.Instance.SetInput(false);

        yield return new WaitForSeconds(0.25f);

        _jumpyAnimator.SetSpriteRenderer(false);
        EventManager.TriggerEvent("FadeIn", new FadeEventParams(1f), this);

        yield return new WaitForSeconds(1f);

        LevelManager.Instance.SpawnPlayer();
        Reset();

        yield return null;
    }

    public void DoknockBack(Vector2 force)
    {
        _jumpyController.enabled = false;
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

    private void Reset()
    {
        EventManager.TriggerEvent("FadeOut", new FadeEventParams(1f), this);
        _death = false;
        _jumpyAnimator.SetSpriteRenderer(true);
        GameManager.Instance.SetInput(true);
        _jumpyController.enabled = true;
        _rigidbody2D.gravityScale = 8;
    }
}
