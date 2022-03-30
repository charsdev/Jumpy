using UnityEngine;
using Chars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jumpy
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class JumpyController : MonoBehaviour
    {
        public bool Grounded => _grounded;
        public event Action<bool> OnGroundedChanged;
        public event Action OnJumping;

        private Rigidbody2D _rb;
        private BoxCollider2D _collider;
        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private float _currentHorizontalSpeed, _currentVerticalSpeed;
        private int _fixedFrame;
        public float TimeOnAir;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            // Calculate velocity
            _velocity = (transform.position - _lastPosition) / Time.deltaTime;
            _lastPosition = transform.position;

            GatherInput();
        }

        void FixedUpdate()
        {
            _fixedFrame++;

            RunCollisionChecks();
            CalculateGravity();
            CalculateWalk();
            CalculateJumpApex();
            CalculateJump();
            RunCornerPrevention();
            CalculateBunnyHop();
        }


        #region Gather Input

        private void GatherInput()
        {
            if (JInput.JumpDown)
            {
                _lastJumpPressed = _fixedFrame;
                _jumpToConsume = true;
            }
        }

        #endregion

        #region Collisions

        [Header("COLLISION")] [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private int _detectorCount = 3;
        [SerializeField] private float _detectionRayLength = 0.1f;

        public struct RayRange
        {
            public RayRange(float x1, float y1, float x2, float y2, Vector2 dir)
            {
                Start = new Vector2(x1, y1);
                End = new Vector2(x2, y2);
                Dir = dir;
            }

            public readonly Vector2 Start, End, Dir;
        }

        private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;

        private bool _hittingCeiling, _grounded, _colRight, _colLeft;

        private float _timeLeftGrounded;

        // We use these raycast checks for pre-collision information
        private void RunCollisionChecks()
        {
            // Generate ray ranges. 
            CalculateRayRanged();

            // Ground
            var groundedCheck = RunDetection(_raysDown);
            if (_grounded && !groundedCheck)
            {
                _timeLeftGrounded = _fixedFrame; // Only trigger when first leaving
                OnGroundedChanged?.Invoke(false);
            }
            else if (!_grounded && groundedCheck)
            {
                TimeOnAir = 0;

                _coyoteUsable = true; // Only trigger when first touching
                _executedBufferedJump = false;
                OnGroundedChanged?.Invoke(true);
            }

            if (!_grounded)
            {
                TimeOnAir += Time.deltaTime;
            }

            _grounded = groundedCheck;
            _colLeft = RunDetection(_raysLeft);
            _colRight = RunDetection(_raysRight);
            _hittingCeiling = RunDetection(_raysUp);

            bool RunDetection(RayRange range)
            {
                return EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _detectionRayLength, _groundLayer));
            }
        }

        private void CalculateRayRanged()
        {
            var b = _collider.bounds;

            _raysDown = new RayRange(b.min.x, b.min.y, b.max.x, b.min.y, Vector2.down);
            _raysUp = new RayRange(b.min.x, b.max.y, b.max.x, b.max.y, Vector2.up);
            _raysLeft = new RayRange(b.min.x, b.min.y, b.min.x, b.max.y, Vector2.left);
            _raysRight = new RayRange(b.max.x, b.min.y, b.max.x, b.max.y, Vector2.right);
        }


        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            for (var i = 0; i < _detectorCount; i++)
            {
                var t = (float)i / (_detectorCount - 1);
                yield return Vector2.Lerp(range.Start, range.End, t);
            }
        }

        private void OnDrawGizmos()
        {
            if (!_collider) _collider = GetComponent<BoxCollider2D>();

            // Rays
            if (!Application.isPlaying) CalculateRayRanged();
            Gizmos.color = Color.blue;
            foreach (var range in new List<RayRange> { _raysDown, _raysUp })
            {
                foreach (var point in EvaluateRayPositions(range))
                {
                    Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                }
            }
        }

        #endregion


        #region Walk

        [Header("WALKING")] [SerializeField] private float _acceleration = 90;
        [SerializeField] private float _moveClamp = 13;
        [SerializeField] private float _deAcceleration = 60f;
        [SerializeField] private float _apexBonus = 2;

        private void CalculateWalk()
        {
            if (JInput.HorizontalInput != 0)
            {
                // Set horizontal move speed
                _currentHorizontalSpeed += JInput.HorizontalInput * _acceleration * Time.fixedDeltaTime;

                // clamped by max frame movement
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);

                // Apply bonus at the apex of a jump
                var apexBonus = Mathf.Sign(JInput.HorizontalInput) * _apexBonus * _apexPoint;
                _currentHorizontalSpeed += apexBonus * Time.fixedDeltaTime;
            }
            else
            {
                // No input. Let's slow the character down
                _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0, _deAcceleration * Time.fixedDeltaTime);
            }

            if (_currentHorizontalSpeed > 0 && _colRight || _currentHorizontalSpeed < 0 && _colLeft)
            {
                // Don't pile up useless horizontal
                _currentHorizontalSpeed = 0;
            }

            if (_currentHorizontalSpeed != 0)
            {
                var idealVel = new Vector3(_currentHorizontalSpeed, _rb.velocity.y);
                _rb.velocity = Vector2.MoveTowards(_rb.velocity, idealVel, _acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region BunnyHop

        [Header("BUNNY HOP")] [SerializeField] private bool CanBunnyHop;
        [SerializeField] private float _bunnyHop = 10f;
        [SerializeField] private float _nextBunnyHop;
        [SerializeField] private float _bunnyHopRate = 0.5f;

        private void CalculateBunnyHop()
        {
            if (JInput.HorizontalInput == 0) return;
            if (!GameManager.Instance.IsInputEnabled) return;

            if (Grounded && Time.time >= _nextBunnyHop && CanBunnyHop)
            {
                _nextBunnyHop = Time.time + _bunnyHopRate;

                _currentVerticalSpeed = _bunnyHop;
                _rb.velocity += (Vector2)transform.up * _currentVerticalSpeed;

            }
        }
        #endregion


        //#region Gravity

        [Header("GRAVITY")] 
        [SerializeField] private float _minFallSpeed = 8f; // normal gravityScale
        [SerializeField] private float _maxFallSpeed = 12f; // early 


        private void CalculateGravity()
        {
            if (_grounded)
            {
                _rb.gravityScale = _minFallSpeed;
            }
            else if (JInput.JumpUp)
            {
                _rb.gravityScale = _maxFallSpeed;
            }

        }

        // #endregion

        #region Jump

        [Header("JUMPING")] [SerializeField] private float _jumpHeight = 30;
        [SerializeField] private float _jumpApexThreshold = 10f;
        [SerializeField] private int _coyoteTimeThreshold = 7;
        [SerializeField] private int _jumpBuffer = 7;
        private bool _jumpToConsume;
        private bool _coyoteUsable;
        private bool _executedBufferedJump;
        private float _apexPoint; // Becomes 1 at the apex of a jump
        private float _lastJumpPressed = Single.MinValue;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _timeLeftGrounded + _coyoteTimeThreshold > _fixedFrame;
        private bool HasBufferedJump => (_grounded || _cornerStuck) && _lastJumpPressed + _jumpBuffer > _fixedFrame && !_executedBufferedJump;

        private void CalculateJumpApex()
        {
            if (!_grounded)
            {
                _apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(_velocity.y));
            }
            else
            {
                _apexPoint = 0;
            }
        }

        private void CalculateJump()
        {
            // Jump if: grounded or within coyote threshold || sufficient jump buffer
            if ((_jumpToConsume && CanUseCoyote) || HasBufferedJump)
            {
                _currentVerticalSpeed = _jumpHeight;
                _rb.velocity = Vector2.zero;
                _rb.velocity += (Vector2)transform.up * _currentVerticalSpeed;

                _coyoteUsable = false;
                _jumpToConsume = false;
                _timeLeftGrounded = _fixedFrame;
                _executedBufferedJump = true;
                OnJumping?.Invoke();
            }

            if (_hittingCeiling && _currentVerticalSpeed > 0) _currentVerticalSpeed = 0;
        }

        #endregion
      

        #region Corner Stuck Prevention

        private Vector2 _lastPos;
        private bool _cornerStuck;

        void RunCornerPrevention()
        {
            _cornerStuck = !_grounded && _lastPos == _rb.position && _lastJumpPressed + 1 < _fixedFrame;
            _currentVerticalSpeed = _cornerStuck ? 0 : _currentVerticalSpeed;
            _lastPos = _rb.position;
        }

        #endregion

    }
}

