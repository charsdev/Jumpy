using UnityEngine;
using Chars.Tools;

namespace Chars
{
    public class EnemyControllerBase : CharacterControllerBase, IPooleable, IKilleable
    {
        [SerializeField] protected Transform Target;
        [SerializeField] protected bool NeedTarget;

        public virtual void Capture() => ObjectPool.Instance?.CaptureFromPool(gameObject, gameObject.name);
        public virtual void Release() => ObjectPool.Instance?.ReturnToPool(gameObject, gameObject.name);
        public virtual void Die() => Release();

        protected override void Awake()
        {
            base.Awake();
            if (NeedTarget) Target = GameManager.Instance?.player.transform;
        }

        protected override void Update()
        {
            base.Update();
            EnemyAILogic();
        }

        protected virtual void EnemyAILogic() { }
    }
}