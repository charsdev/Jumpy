using UnityEngine;
using Chars.Tools;

namespace Chars
{
    public class EnemyShooter : EnemyControllerBase
    {
        [SerializeField] private Transform _firepoint;
        private float _nextShoot = 2f;
        [SerializeField] private float _rate = 2f;

        protected override void EnemyAILogic()
        {
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

    }
}