using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    public void Die()
    { 
        OnDie.Invoke(); 
    }
}
