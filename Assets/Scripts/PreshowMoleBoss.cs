using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreshowMoleBoss : StateMachineBehaviour
{
    private float _timeToShow = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeToShow -= Time.deltaTime;

        if (_timeToShow <= 0)
        {
            animator.SetTrigger("Show");
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Show");
        _timeToShow = 2f;
    }
}
