using UnityEngine;
using Chars.Tools;
using UnityEngine.Events;

namespace Jumpy
{
    public class EnemyShooter : MonoBehaviour, IKilleable, IPooleable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _range;
        [SerializeField] private Transform _firepoint;
        private float _nextShoot = 2f;
        [SerializeField] private float _rate = 2f;
        [SerializeField] private SpriteRenderer spriteRenderer; 

        private void Start()
        {
            _target = GameManager.Instance.player.transform;
        }

        private void Update()
        {     

            Vector3 distance = _target.position - transform.position;

            // I comment this for objectPool testing porpuses
            /*if (DebugTool.RaycastHit(transform.position, distance.normalized.x * Vector2.right, 15f, LayerMask.GetMask("Player"), Color.red, true))
            {
                if (Time.time >= _nextShoot)
                {
                    Shoot();
                    _nextShoot = Time.time + _rate;
                }
            }*/

            if (Time.time >= _nextShoot)
            {
                Shoot();
                _nextShoot = Time.time + _rate;
            }
        }

        private void Shoot()
        {
            GameObject instance = ObjectPool.Instance.GetPooledObject("Shot");
            instance.transform.position = _firepoint.transform.position;
            instance.transform.rotation = _firepoint.transform.rotation;
        }

        public void Die()
        {
            Release();
        }

        public void Release()
        {
            ObjectPool.Instance.ReturnToPool(gameObject, "EnemyShooter");
        }

        public void Capture()
        {
            ObjectPool.Instance.CaptureFromPool(gameObject, "EnemyShooter");
        }
    }
}