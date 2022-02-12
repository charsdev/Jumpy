using UnityEngine;

namespace Jumpy
{
    public class JumpyAnimator : MonoBehaviour
    {
        [SerializeField] public Animator Animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AudioSource _source;
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [SerializeField] private AudioClip[] _footsteps;

        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxParticleFallSpeed = -40;

        private JumpyController _player;
        private bool _playerGrounded;
        private Vector2 _movement;

        void Awake() => _player = GetComponent<JumpyController>();

        void Update()
        {
            if (GameManager.Instance.GameIsPaused) return;
            if (_player == null) return;

            // Flip the sprite
            if (JInput.HorizontalInput != 0)
            {
                _spriteRenderer.flipX = JInput.HorizontalInput < 0;
            }

            // Speed up idle while running
            Animator.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, Mathf.Abs(JInput.HorizontalInput)));

            // Splat
            if (_player.LandingThisFrame)
            {
                Animator.SetTrigger(GroundedKey);
                if (_footsteps.Length > 0)
                    _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
            }

            // Jump effects
            if (_player.JumpingThisFrame)
            {
                Animator.SetTrigger(JumpKey);
                Animator.ResetTrigger(GroundedKey);

                // Only play particles when grounded (avoid coyote)
                if (_player.OnGround)
                {
                    _jumpParticles.Play();
                }
            }

            // Play landing effects and begin ground movement effects
            if (!_playerGrounded && _player.OnGround)
            {
                _playerGrounded = true;
                _moveParticles.Play();
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, _maxParticleFallSpeed, _movement.y);
                _landParticles.Play();
            }
            else if (_playerGrounded && !_player.OnGround)
            {
                _playerGrounded = false;
                _moveParticles.Stop();
            }

            _movement = GetComponent<CharacterBody>().Rigidbody2D.velocity;
            //_movement = _player.deltaMovement; // Previous frame movement is more valuable
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

        #endregion
    }
}