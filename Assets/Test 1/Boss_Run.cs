using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

	public float speed = 2.5f;
	private Transform _player;
	private Rigidbody2D _rigidBody;
	private Boss _boss;
	private Transform parent;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		parent = animator.transform.parent;
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_rigidBody = parent.GetComponent<Rigidbody2D>();
		_boss = parent.GetComponent<Boss>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_boss.LookAtPlayer();

		float distanceX = _player.position.x - animator.transform.position.x; 

		if (Mathf.Abs(distanceX) >= 0.5f)
        {
            Vector2 target = new Vector2(_player.position.x, _rigidBody.position.y);

            Vector2 newPos = Vector2.MoveTowards(_rigidBody.position, target, speed * Time.fixedDeltaTime);
            _rigidBody.MovePosition(newPos);
		}
		else
		{
			animator.SetTrigger("Show");
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Show");
	}
}
