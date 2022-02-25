using Chars.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Chars
{
    public class EnemyPatrol : EnemyControllerBase
    {
        [SerializeField] private List<Transform> _points = new List<Transform>();
        //[SerializeField] private float _speed = 5f;
        //private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _timeToSide = 0;
        private int _index = 0;
        [SerializeField] private float costOfTime = 5f;

        protected override void Start()
        {
            base.Start();
            Target = _points[0];
        }

        protected override void EnemyAILogic()
        {
            _timeToSide -= Time.deltaTime;

            if (_timeToSide <= 0)
            {
                _index = _index < _points.Count - 1 ? _index + 1 : 0;
                Target = _points[_index];
                _timeToSide = costOfTime;
            }

            if (Target != null)
            {
                CharacterBody.Rigidbody2D.position = Vector2.MoveTowards(CharacterBody.Rigidbody2D.position, Target.position, CharacterData.Speed * Time.deltaTime);
            }

        }
    }

}
