using UnityEngine;

public class BossAttack : MonoBehaviour
{
	public GameObject hitbox;

	public void ShowHitbox()
	{
		hitbox.SetActive(true);
	}

	public void HideHitbox()
	{
		hitbox.SetActive(false);
	}
}
