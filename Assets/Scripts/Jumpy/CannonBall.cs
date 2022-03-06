using UnityEngine;
using Chars;

namespace Jumpy
{
    public class CannonBall : MonoBehaviour
    {
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

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _echoEffect = GetComponent<EchoEffect>();
            _squash = GetComponent<Squash>();
            _animator = GetComponent<Animator>();
            _currentPoint = 0;
            _color = Color.white;
            _firepoint.transform.SetParent(null);
        }

        private void Update()
        {

            Charge();

            SetDirection();
            SetAngle();
            LockFirepoint();
            Shoot();
        }

        private void Shoot()
        {
            if (JInput.PowerJumpUp)
            {
                _rigidBody.velocity = _velocity;
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

        private void SetAngle()
        {
            if (Camera != null && _firepoint.gameObject.activeInHierarchy)
            {
                Vector3 diff = (Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                _angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                if (_animator != null && !_animator.enabled && _spriteRenderer != null)
                    _spriteRenderer.flipX = _angle > MaxAngle;
            }
        }

        private void Charge()
        {
            _velocity = new Vector2(CurrentPower * _direction.x, CurrentPower * _direction.y);
           
            if (JInput.PowerJumpDown && _animator.enabled)
            {
                _animator.enabled = false;
            }

            if (JInput.PowerJumpUpHeld) 
            {
                _targetScale -= _reduceScale * Time.deltaTime;
                _targetScale = Mathf.Max(_targetScale, _reduceScale);
                _spaceButtonCounter += Time.deltaTime;

                Squash(_targetScale);

                if (_spaceButtonCounter > TimeToInit)
                {
                    SetLightOn();

                    if (_spaceButtonCounter > _timeToNextPoint)
                    {
                        _currentPoint = _currentPoint < _points.Length - 1 ? _currentPoint + 1 : _points.Length - 1;
                        _timeToNextPoint += 0.5f;
                        CurrentPower = PowerAmount[_currentPoint];
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
            SetLightOff();
            Squash(_targetScale);
        }

        private void Squash(float value)
        {
            if (_squash != null && _squash.enabled)
                _squash.SquashScale(value);
        }

        private void SetDirection()
        {
            if (Camera != null && _firepoint.gameObject.activeInHierarchy)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _direction = ((Vector3)mousePos - transform.position).normalized;
            }
        }

        private void SetLightOn()
        {
            _firepoint.gameObject.SetActive(true);
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

            if (_echoEffect != null)
                _echoEffect.enabled = false;

            if (!JInput.PowerJumpUpHeld)
            {
                _animator.enabled = true;
            }
        }
    }
}