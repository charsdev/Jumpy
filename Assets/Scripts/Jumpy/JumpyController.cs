using UnityEngine;
using Chars;

namespace Jumpy
{
    public class JumpyController : CharacterControllerBase
    {
        private const int VerticalMaxHeight = 28;
        public bool OnGround;
        [SerializeField] private float _jumpHeight = 30f;
        private float _deltaX;
        public bool LandingThisFrame { get; internal set; }
        public bool JumpingThisFrame { get; internal set; }
        public float DeltaX { get => _deltaX; }
        public bool CanMove;
        public bool CanWallJump;
        public bool CanWallSlide;
        public float HighJumpScale = 8f;
        public float LowJumpScale = 21f;
        public float ClampHorizontalSpeed = 13f;
        public float ClampVerticalSpeed = -40f;
        public float HorizontalAcceleration = 90f;
        public float HorizontalDecceleration = 60f;
        [SerializeField] private float _bunnyHop = 10f;
        [SerializeField] private float _nextBunnyHop;
        [SerializeField] private float _bunnyHopRate = 0.5f;
        private float coyoteTime;
        public bool EnableBunnyHop = false;
        public bool limitVerticalSpeed;
        public bool BlockMoveOnAir;

        protected override void Awake()
        {
            base.Awake();
            CanMove = true;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            SetJumpHeigth();
            SetVerticalSpeed();
        }

        private void SetVerticalSpeed()
        {
            if (CharacterBody.Rigidbody2D.velocity.y < ClampVerticalSpeed)
            {
                CharacterBody.Rigidbody2D.velocity = new Vector2(CharacterBody.Rigidbody2D.velocity.x, ClampVerticalSpeed);
            }

            if (limitVerticalSpeed)
            {
                if (CharacterBody.Rigidbody2D.velocity.y > VerticalMaxHeight)
                {
                    CharacterBody.Rigidbody2D.velocity = new Vector2(CharacterBody.Rigidbody2D.velocity.x, VerticalMaxHeight);
                }
            }
        }

        private void SetJumpHeigth()
        {
            if (CharacterBody.Rigidbody2D.velocity.y < 0)
            {
                CharacterBody.GravityModifier = HighJumpScale;
            }
            else if (CharacterBody.Rigidbody2D.velocity.y > 0 && JInput.JumpUp)
            {
                CharacterBody.GravityModifier = LowJumpScale;
            }
        }

        protected override void Update()
        {
            if (OnGround && limitVerticalSpeed)
            {
                limitVerticalSpeed = false;
            }

            Landing();
            HorizontalMovement();
            Jump();
            WallJump();
            BunnyHop();

            if(JInput.JumpDown)
            {
                limitVerticalSpeed = true;
            }
        }

        private void Landing()
        {
            LandingThisFrame = false;
            var groundCheck = CharacterBody.OnGround;
            LandingThisFrame = groundCheck && !OnGround;
            OnGround = groundCheck;
            coyoteTime = OnGround ? 0 : coyoteTime + Time.deltaTime;
        }

        private void BunnyHop()
        {
            if (JInput.HorizontalInput == 0) return;
            if (!CanMove) return;
            if (!GameManager.Instance.IsInputEnabled) return;

            if (OnGround && Time.time >= _nextBunnyHop && EnableBunnyHop)
            {
                _nextBunnyHop = Time.time + _bunnyHopRate;
                CharacterBody.Rigidbody2D.velocity += (Vector2)CharacterBody.Up * _bunnyHop;
            }
        }

        private void WallJump()
        {
            if (CharacterBody.OnWall && !OnGround && JInput.JumpDown)
            {
                CharacterBody.Rigidbody2D.velocity = Vector2.zero;
                CharacterBody.Rigidbody2D.AddForce(new Vector2(CharacterBody.wallSide * 15, 30), ForceMode2D.Impulse);
            }
        }

        private void HorizontalMovement()
        {
            if (BlockMoveOnAir) return;
            if (!CanMove) return;
            if (!GameManager.Instance.IsInputEnabled) { return; };

            if (JInput.HorizontalInput != 0)
            {
                _deltaX += JInput.HorizontalInput * HorizontalAcceleration * Time.deltaTime;
                _deltaX = Mathf.Clamp(_deltaX, -ClampHorizontalSpeed, ClampHorizontalSpeed);
            }
            else
            {
                _deltaX = Mathf.MoveTowards(_deltaX, 0, HorizontalDecceleration * Time.deltaTime);
            }

            if (_deltaX != 0)
            {
                var idealVel = new Vector3(_deltaX, CharacterBody.Rigidbody2D.velocity.y);
                CharacterBody.Rigidbody2D.velocity = Vector2.MoveTowards(CharacterBody.Rigidbody2D.velocity, idealVel, HorizontalAcceleration * Time.deltaTime);
            }
        }

        private void Jump()
        {
            if (JInput.JumpDown && (OnGround || coyoteTime < 0.25f) && CanMove)
            {
                CharacterBody.Rigidbody2D.velocity = Vector2.zero; //force velocity 0;
                CharacterBody.Rigidbody2D.velocity += (Vector2)CharacterBody.Up * _jumpHeight;
                JumpingThisFrame = true;
            }
            else
            {
                JumpingThisFrame = false;
            }
        }
       

    }
}

