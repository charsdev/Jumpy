using Chars;
using UnityEngine;

namespace Jumpy
{
    public class JumpyAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioSource _source;
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [SerializeField] private AudioClip[] _footsteps;

        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxParticleFallSpeed = -40;

        private JumpyController _player;
        [SerializeField] private JumpyHealth _playerHealth;
        private Vector2 _movement;

        void Awake()
        {
            _player = GetComponentInParent<JumpyController>();

            _player.OnGroundedChanged += OnLanded;
            _player.OnJumping += OnJumping;
            _playerHealth?.OnHit.AddListener(OnHit);
        }

        void OnDestroy()
        {
            _player.OnGroundedChanged -= OnLanded;
            _player.OnJumping -= OnJumping;
            _playerHealth?.OnHit.RemoveListener(OnHit);
        }


        #region Extended

        #endregion

        private void OnJumping()
        {
            if (_anim != null)
            {
                _anim.SetTrigger(JumpKey);
                _anim.ResetTrigger(GroundedKey);
            }
               

            // Only play particles when grounded (avoid coyote)
            if (_player.Grounded)
            {
                _jumpParticles?.Play();
            }
        }

        private void OnLanded(bool grounded)
        {
            if (grounded)
            {
                if (_anim != null)
                    _anim.SetTrigger(GroundedKey);

                if (_footsteps.Length > 0)
                {                    
                    _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
                }

                _moveParticles?.Play();
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, _maxParticleFallSpeed, _movement.y);
                _landParticles?.Play();
            }
            else
            {
                _moveParticles.Stop();
            }
        }

        private void OnHit()
        {
            if (_anim != null)
                _anim.SetTrigger(Hit);
        }

        void Update()
        {
            if (_player == null) return;

            // Flip the sprite
            if (JInput.HorizontalInput != 0 && GameManager.Instance.IsInputEnabled)
            {
                _spriteRenderer.flipX = JInput.HorizontalInput < 0;
            }

            // Speed up idle while running
            if (_anim != null)
                _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, Mathf.Abs(JInput.HorizontalInput)));
        }

        public void SetSpriteRenderer(bool value)
        {
            _spriteRenderer.enabled = value;
        }
  
        private void OnDisable()
        {
            _moveParticles.Stop();
        }

        private void OnEnable()
        {
            _moveParticles.Play();
        }

        #region Animation Keys

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int Hit = Animator.StringToHash("Hit");

        #endregion
    }
}