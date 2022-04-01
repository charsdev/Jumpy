using UnityEngine;
using DG.Tweening;

public class Boss_Show : StateMachineBehaviour
{
    public float Speed;
    public float moveInY;
    private Boss _boss;
    private Transform parent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.transform.parent;
        _boss = parent.GetComponent<Boss>();
        animator.transform.DOMoveY(moveInY, Speed).OnComplete(() => animator.SetTrigger("Attack"));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = new Vector3(animator.transform.position.x, animator.transform.position.y, 0f);
        animator.ResetTrigger("Attack");
    }

}
