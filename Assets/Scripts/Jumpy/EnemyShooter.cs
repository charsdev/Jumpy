using UnityEngine;
using Chars.Tools;

namespace Jumpy 
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _target;
        [SerializeField] private float _range;
        [SerializeField] private Transform _firepoint;
        [SerializeField] private float _nextShoot = 0f;
        [SerializeField] private float _rate = 2f;
        private bool _facingRight;

        private void Start() => _target = GameObject.FindGameObjectWithTag("Player").transform;

        private void Update()
        {
            Vector3 distance = _target.position - transform.position;

            if (DebugTool.RaycastHit(transform.position, distance.normalized.x * Vector2.right, 15f, LayerMask.GetMask("Player"), Color.red, true))
            {
                if (Time.time >= _nextShoot)
                {
                    Shoot();
                    _nextShoot = Time.time + _rate;
                }
            }
        }

        private void Shoot()
        {
            Instantiate(_prefab, _firepoint.transform.position, _firepoint.transform.rotation);
        }
    }
}