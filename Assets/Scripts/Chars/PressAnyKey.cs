using UnityEngine;
using UnityEngine.Events;
public class PressAnyKey : MonoBehaviour
{
    public UnityEvent AnyKeydownEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            AnyKeydownEvent.Invoke();
        }
    }
}
