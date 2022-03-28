using UnityEngine;
using DG.Tweening;
public class Boss_HideBody : StateMachineBehaviour
{
    public float Speed;
    public float moveInY;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = new Vector3(animator.transform.position.x, animator.transform.position.y, 0.5f);
        animator.transform.DOMoveY(moveInY, Speed).OnComplete(() => animator.SetTrigger("Under"));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Under");
    }

}
