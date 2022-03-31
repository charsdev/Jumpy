using UnityEngine;
using Cinemachine;
using UnityEngine.Events;


public class Room : MonoBehaviour
{
    public GameObject VirtualCamera;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Awake()
    {
        _cinemachineVirtualCamera = VirtualCamera.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            OnEnter.Invoke();
            if (_cinemachineVirtualCamera.Follow == null)
            {
                _cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
            }
            VirtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            OnExit.Invoke();

            VirtualCamera.SetActive(false);
        }
    }

}
