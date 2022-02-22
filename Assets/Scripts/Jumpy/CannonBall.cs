using UnityEngine;
using Chars;

namespace Jumpy
{
    public class CannonBall : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] float _offset;
        [SerializeField] float _angle;
        [SerializeField] private Transform _firepoint;
        [SerializeField] private int _amount;
        [SerializeField] private int _currentLimit = 0;
        [SerializeField] private int _nextLimit;
        [SerializeField] private int _maxLimit;
        [SerializeField] private SpriteRenderer[] _points;
        [SerializeField] private Vector2 _direction;
        private EchoEffect _echoEffect;
        private int _currentPoint;
        private Rigidbody2D _rigidBody;
        private Color _color;
        private Vector3 _velocity = Vector3.zero;
        [SerializeField] private float _spaceButtonCounter;
        [SerializeField] private Transform _model;
        private GameObject _prefabFirepoint;
        public JumpyController JumpyController;
        private float _targetScale = 1;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private JumpyAnimator _jumpyAnimator;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _echoEffect = GetComponent<EchoEffect>();
            _currentPoint = 0;
            _nextLimit = _amount;
            _maxLimit = _amount * _points.Length;
            _color = Color.white;
            _firepoint.transform.SetParent(null);
        }
   
        private void Update()
        {
            SetDirection();
            SetAngle();
            LockFirepoint();
            Shoot();
        }

        private void LockFirepoint()
        {
            var x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _offset;
            var y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _offset;
            _firepoint.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
            _firepoint.transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void SetAngle()
        {
            if (Camera.main != null)
            {
                Vector3 diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                _angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                if (!_jumpyAnimator.Animator.enabled)
                    _spriteRenderer.flipX = _angle > 90;
            }
        }

        private void Shoot()
        {
            _velocity = new Vector2(_force * _direction.x, _force * _direction.y);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _jumpyAnimator.Animator.enabled = false;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
            {
                _targetScale -= 0.8f * Time.deltaTime ;
                if (_targetScale < 0.8f)
                {
                    _targetScale = 0.8f;
                }

                JumpyController.SquashScale(_targetScale);

                _spaceButtonCounter += Time.deltaTime;
                if (_spaceButtonCounter > 0.25f)
                {
                    _force += _amount * 2.5f * Time.deltaTime;

                    if (_force > _currentLimit && _force < _nextLimit)
                    {
                        SetLightOn();
                        _currentLimit = Mathf.Clamp(_currentLimit, _currentLimit + _amount, _maxLimit);
                        _nextLimit = Mathf.Clamp(_nextLimit, _nextLimit + _amount, _maxLimit);
                        if (_nextLimit > _maxLimit)
                        {
                            _nextLimit = _maxLimit;
                        }
                        _currentPoint++;
                    }
                    if (_force > _maxLimit)
                    {
                        _force = _maxLimit;
                    }
                }
               
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) //TODO: Change to Input
            {
                Time.timeScale = 1;
                _echoEffect.enabled = true;
                _rigidBody.velocity = _velocity;
                SetLightOff();
                _currentPoint = 0;
                _currentLimit = 0;
                _nextLimit = _amount;
                _force = 0;
                _spaceButtonCounter = 0;
                _velocity = Vector3.zero;
                _direction = Vector3.zero;
                _targetScale = 1;
                JumpyController.SquashScale(_targetScale);
                _rigidBody.gravityScale = 8;
            }
        }

        private void SetDirection()
        {
            if (Camera.main != null)
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
            if (!enabled)
            {
                return;
            }

            if (!Input.GetKey(KeyCode.S))
            { //Change 
                _jumpyAnimator.Animator.enabled = true;
            }
            _echoEffect.enabled = false;
        }
    }
}