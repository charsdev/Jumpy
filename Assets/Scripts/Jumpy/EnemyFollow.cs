using UnityEngine;
using Chars.Tools;

namespace Chars
{
    public class EnemyFollow : EnemyControllerBase
    {
        public Animator Animator;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
        }

        protected override void EnemyAILogic()
        {
            if (Target == null) return;

            Vector2 delta = Target.transform.position - transform.position;
            float deltaX = delta.normalized.x;
            Vector2 direction = deltaX * Vector2.right;
            RaycastHit2D hit = DebugTool.RaycastHit(
                transform.position, 
                direction,
                5,
                LayerMask.GetMask("Player"),
                Color.red,
                true
            );

            if (hit)
            {
                Animator.SetBool("Running", true);

                CharacterBody.Rigidbody2D.velocity = Vector2.MoveTowards(
                    CharacterBody.Rigidbody2D.velocity, 
                    new Vector2(
                        deltaX * CharacterData.Speed,
                        CharacterBody.Rigidbody2D.velocity.y),
                        CharacterData.Speed * Time.deltaTime
                    );
            }
            else
            {
                Animator.SetBool("Running", false);
            }

            if (Target.transform.position.x < gameObject.transform.position.x && FacingRight)
            {
                Flip(true);
            }

            if (Target.transform.position.x > gameObject.transform.position.x && !FacingRight) 
            {
                Flip(true);
            }
        }
    }
}