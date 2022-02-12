using System.Collections.Generic;
using UnityEngine;

namespace Chars
{
    public class FollowTheLeader : MonoBehaviour
    {
        public Transform Leader;
        public int Steps;
        private Queue<Vector3> _record = new Queue<Vector3>();

        private void LateUpdate()
        {
            _record.Enqueue(Leader.position);
            if (_record.Count > Steps)
            {
                transform.position = _record.Dequeue();
            }
        }
    }
}
