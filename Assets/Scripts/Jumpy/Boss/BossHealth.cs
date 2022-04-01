using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	public int health = 500;
	public Animator Animator;

	public void TakeDamage(int damage)
	{
		health -= damage;
		Animator.SetTrigger("Damage");

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

}
