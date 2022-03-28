using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	private Transform _player;
	public bool IsFlipped = false;

	private void Start()
    {
		_player = GameManager.Instance.player.transform;
	}

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > _player.position.x && IsFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			IsFlipped = false;
		}
		else if (transform.position.x < _player.position.x && !IsFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			IsFlipped = true;
		}
	}


}
