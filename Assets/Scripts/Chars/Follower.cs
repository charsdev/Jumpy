using System.Collections.Generic;
using UnityEngine;

namespace Chars
{
    public class Follower : MonoBehaviour
    {
        public Transform Leader;
        public int Steps;
        private Queue<Vector3> _record = new Queue<Vector3>();

        //Todo: Really necessary or just fancy?
        private Transform _transform;
        public Transform Transform => _transform;

        private void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            //TODO: Avoid Nulls, try use NullableObject
            if (Leader == null) return;
            
            _record.Enqueue(Leader.position);
            if (_record.Count > Steps)
            {
                transform.position = _record.Dequeue();
            }
        }

        //Act Like a Constructor
        public void Setup(Transform leader, int steps)
        {
            Leader = leader;
            Steps = steps;
        }
    }
}
