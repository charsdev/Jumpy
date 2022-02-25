using Jumpy;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public void Start()
    {
    }

    public void Die()
    {
        RespawnSystem.Instance.Respawn();
    }
}
