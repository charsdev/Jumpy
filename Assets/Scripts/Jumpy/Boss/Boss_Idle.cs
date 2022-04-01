using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float AttackRange = 3f;
    Transform player;
    public float timerToNextMovement = 2f;
    private float initialTimer;
    private Boss _boss;
    private Transform parent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.transform.parent;
        _boss = parent.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialTimer = 2f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();

        timerToNextMovement -= Time.deltaTime;
        if (timerToNextMovement <= 0)
        {
            float distanceX = Mathf.Abs(player.position.x - animator.transform.position.x);

            if (distanceX <= AttackRange)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetTrigger("Hide");
            }

            timerToNextMovement = initialTimer;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hide");

        timerToNextMovement = initialTimer;
    }

}
