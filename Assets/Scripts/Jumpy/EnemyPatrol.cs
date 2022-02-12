using System.Collections.Generic;
using UnityEngine;

namespace Jumpy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed = 5f;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _timeToSide = 0;
        private int _index = 0;
        [SerializeField] private float costOfTime = 5f;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _timeToSide -= Time.deltaTime;

            if (_timeToSide <= 0)
            {
                _index = _index < _points.Count - 1 ? _index + 1 : 0;
                _target = _points[_index];
                _timeToSide = costOfTime;
            }

            if (_target != null)
            {
                _rigidbody2D.position = Vector2.MoveTowards(_rigidbody2D.position, _target.position, _speed * Time.deltaTime);
            }

        }
    }

}
