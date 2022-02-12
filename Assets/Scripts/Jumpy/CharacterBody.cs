using UnityEngine;
using Chars.Tools;
using System;

namespace Jumpy 
{
    public class CharacterBody : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D { get; private set; }
        public BoxCollider2D BoxCollider2D;

        public Vector3 Down { get => _down; }
        public Vector3 Left { get => _left; }
        public Vector3 Right { get => _right; }
        public Vector3 Up { get => _up; }
        public int Sharpness = 16; // Sharpness of ground Collision

        public float RotationSpeed = 5f;
        public bool useRigidBodyGravityScale = true;

        public enum Side
        {
            DOWN, RIGHT, LEFT, UP
        }

        [Header("Gravity Direction")]
        public Side CurrentSide;

        private void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            BoxCollider2D = GetComponent<BoxCollider2D>();
            groundBuffer = new RaycastHit2D[Sharpness];
        }

        private void FixedUpdate()
        {
            if (useRigidBodyGravityScale)
            {
                Rigidbody2D.gravityScale = GravityModifier;
            }
            else 
            {
                Rigidbody2D.AddForce(GravityVector * GravityModifier);
            }
        }

        private void Update()
        {
            HandleDirection();
            HandleCollision();

            Rigidbody2D.gravityScale = useRigidBodyGravityScale ? GravityModifier : 0;
        }

        [Header("Layers")]
        public LayerMask groundLayer;

        [Header("Collision")]

        [Space]
        public bool OnAbove;
        public bool OnGround;
        public bool OnWall;
        public bool OnRightWall;
        public bool OnLeftWall;
        public int wallSide;

        [Space]
        public float collisionRadius = 0.25f;
        public Color DebugCollisionColor = Color.red;
        public float OffsetLength = 0.5f;
        
        //public readonly Collider2D[] _ground = new Collider2D[1];
        public readonly Collider2D[] _rightWall = new Collider2D[1];
        public readonly Collider2D[] _leftWall = new Collider2D[1];
        public readonly Collider2D[] _aboveWall = new Collider2D[1];

        public RaycastHit2D[] groundBuffer;

        private void HandleCollision()
        {

            //OnGround = Physics2D.OverlapCircleNonAlloc(transform.position + -transform.up * OffsetLength, collisionRadius, _ground, groundLayer) > 0;
            if (groundBuffer.Length != Sharpness)
            {
                groundBuffer = new RaycastHit2D[Sharpness];
            }

            for (int i = 0; i < Sharpness; i++)
            {
                Vector2 originPoint = Vector2.Lerp(BoxCollider2D.bounds.min, BoxCollider2D.bounds.max - transform.up, (float)i / (float)(Sharpness - 1));
                groundBuffer[i] = DebugTool.RaycastHit(originPoint, -transform.up, OffsetLength, groundLayer, Color.blue, true);
            }

            OnGround = Array.Exists(groundBuffer, element => element == true);


            OnRightWall = Physics2D.OverlapCircleNonAlloc(transform.position + transform.right * OffsetLength, collisionRadius, _rightWall, groundLayer) > 0;
            OnLeftWall = Physics2D.OverlapCircleNonAlloc(transform.position + -transform.right * OffsetLength, collisionRadius, _leftWall, groundLayer) > 0;
            OnAbove = Physics2D.OverlapCircleNonAlloc(transform.position + transform.up * OffsetLength, collisionRadius, _aboveWall, groundLayer) > 0;
            OnWall = OnRightWall || OnLeftWall;
            wallSide = OnRightWall ? -1 : 1;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = DebugCollisionColor;
            Gizmos.DrawWireSphere(transform.position + _right * OffsetLength, collisionRadius);
            Gizmos.DrawWireSphere(transform.position + _left * OffsetLength, collisionRadius);
            Gizmos.DrawWireSphere(transform.position + _up * OffsetLength, collisionRadius);
        }

        private Vector3 _down, _left, _right, _up;
        public Vector2 GravityVector;
        public float GravityAcceleration = -9.8f;
        public float GravityModifier = 1;
        public float angle;

        //Next can be handle Direction by angle of slope
        private void HandleDirection()
        {
            switch (CurrentSide)
            {
                case Side.UP:
                    _down = Vector3.up;
                    _right = Vector3.left;
                    _left = Vector3.right;
                    _up = Vector3.down;
                    angle = 180f;
                    GravityVector = new Vector2(0, -GravityAcceleration);
                    break;
                case Side.RIGHT:
                    _down = Vector3.right;
                    _right = Vector3.up;
                    _left = Vector3.down;
                    _up = Vector3.left;
                    angle = 90f;
                    GravityVector = new Vector2(-GravityAcceleration, 0);
                    break;
                case Side.DOWN:
                    _down = Vector3.down;
                    _left = Vector3.left;
                    _right = Vector3.right;
                    _up = Vector3.up;
                    angle = 0;
                    GravityVector = new Vector2(0, GravityAcceleration);
                    break;
                case Side.LEFT:
                    _down = Vector3.left;
                    _right = Vector3.down;
                    _left = Vector3.up;
                    _up = Vector3.right;
                    angle = -90f;
                    GravityVector = new Vector2(GravityAcceleration, 0);
                    break;
            }
        }
    }
}