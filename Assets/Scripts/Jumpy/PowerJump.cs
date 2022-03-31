using UnityEngine;
using Chars;

namespace Jumpy
{
    public class PowerJump : MonoBehaviour
    {
        public bool showCursor = false;
        public float SmoothDamping = 80f;

        public float[] PowerAmount;
        public float IncreaseSpeed = 2.5f;
        public float TimeToInit = 0.25f;
        public int MaxAngle = 90;
        public Camera Camera;

        [SerializeField] private float _offset;
        [SerializeField] private float _angle;
        [SerializeField] private Transform _firepoint;
        [SerializeField] private SpriteRenderer[] _points;
        private Vector2 _direction;
        private EchoEffect _echoEffect;
        [SerializeField] private int _currentPoint;
        private Rigidbody2D _rigidBody;
        private bool _hasEchoEffect;
        private bool _hasSquash;
        private bool _hasAnimator;
        private Color _color;
        private Vector3 _velocity = Vector3.zero;
        [SerializeField] private float _spaceButtonCounter;
        private float _targetScale = 1;
        [SerializeField] private float _reduceScale = 0.8f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Squash _squash;
        private float _timeToNextPoint = 0.5f;
        public float CurrentPower;
        public bool WasShooted;
        private JumpyController _jumpyController;
        private float CurrentVelocity;
        public bool MouseControl = true;
        private float _timeFromShoot;

        private void Start()
        {
            Camera = GameManager.Instance.mainCamera;
            _rigidBody = GetComponent<Rigidbody2D>();
            _jumpyController = GetComponent<JumpyController>();
            _hasEchoEffect = TryGetComponent(out _echoEffect);
            _hasSquash = TryGetComponent(out _squash);
            _hasAnimator = TryGetComponent(out _animator);
            _currentPoint = 0;
            _color = Color.white;
            _firepoint.transform.SetParent(null);
            _angle = MaxAngle;
        }

        private void Update()
        {

            if (WasShooted)
            {
                return;
            }

            Charge();
            SetDirection();
            SetAngle();
            LockFirepoint();
            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton4))
            {
                _rigidBody.velocity = _velocity;
                WasShooted = true;
                Reset();
            }
        }

        private void LockFirepoint()
        {
            if (!_firepoint.gameObject.activeInHierarchy) return;

            var x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _offset;
            var y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _offset;

            _firepoint.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
            _firepoint.transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        public static float Clamp0360(float eulerAngles)
        {
            float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
            if (result < 0)
            {
                result += 360f;
            }
            return result;
        }

        public Vector3 startMousePosition;

        private void SetAngle()
        {
            if (Camera != null && _firepoint.gameObject.activeInHierarchy)
            {
                Vector3 mousePositionRespectPlayer = (Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                float mouseAngle = Mathf.Atan2(mousePositionRespectPlayer.y, mousePositionRespectPlayer.x) * Mathf.Rad2Deg;

                if (MouseControl)
                {
                    _angle = mouseAngle;
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        _angle += 200f * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        _angle -= 200f * Time.deltaTime;
                    }

                    _angle -= Input.GetAxis("RightJoystick") * 200f * Time.deltaTime;
                }

                if (Mathf.Abs(Input.GetAxis("RightJoystick")) > 0)
                {
                }

                if (_hasAnimator && !_animator.enabled && _spriteRenderer != null)
                    _spriteRenderer.flipX = _angle > MaxAngle;
            }
        }

        private void Charge()
        {
            _velocity = new Vector2(CurrentPower * _direction.x, CurrentPower * _direction.y);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)) {

                _firepoint.transform.rotation = Quaternion.Euler(0, 0, _angle);
                _jumpyController.enabled = false;

                if (_hasAnimator && _animator.enabled)
                {
                    _animator.enabled = false;
                }
            }  

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton4)) 
            {
                _targetScale -= _reduceScale * Time.deltaTime;
                _targetScale = Mathf.Max(_targetScale, _reduceScale);
                _spaceButtonCounter += Time.deltaTime;

                Squash(_targetScale);

                if (_spaceButtonCounter > TimeToInit)
                {
                    SetLightOn();
                    CurrentPower = PowerAmount[_currentPoint];

                    if (_spaceButtonCounter > _timeToNextPoint)
                    {
                        _currentPoint = _currentPoint < _points.Length - 1 ? _currentPoint + 1 : 0;
                        _timeToNextPoint += 0.5f;
                    }
                }
            }
        }

        private void Reset()
        {
            if (_echoEffect != null)
                _echoEffect.enabled = true;

            _currentPoint = 0;

            _timeToNextPoint = 0.5f;
            CurrentPower = 0;
            _spaceButtonCounter = 0;
            _velocity = Vector3.zero;
            _direction = Vector3.zero;
            _targetScale = 1;
            _rigidBody.gravityScale = 8;
            _jumpyController.enabled = true;
            SetLightOff();
            Squash(_targetScale);
            _angle = MaxAngle;
            _firepoint.transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void Squash(float value)
        {
            if (_hasSquash && _squash.enabled)
                _squash.SquashScale(value);
        }

        private void SetDirection()
        {
            if (Camera != null && _firepoint.gameObject.activeInHierarchy)
            {
                _direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)).normalized;
            }
        }

        private void SetLightOn()
        {
            _firepoint.gameObject.SetActive(true);
            
            Color temp = _color;
            temp.a = 0.5f;

            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].color = temp;
            }

            _points[_currentPoint].color = _color;
        }

        private void SetLightOff()
        {
            Color temp = _color;
            temp.a = 0.5f;

            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].color = temp;
            }

            _firepoint.gameObject.SetActive(false);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!enabled) return;

            if (WasShooted)
            {
                WasShooted = false;
                _jumpyController.enabled = true;
            }

            if (_hasEchoEffect)
                _echoEffect.enabled = false;

            if (!JInput.PowerJumpUpHeld && _hasAnimator)
            {
                _animator.enabled = true;
            }
        }
    }


}