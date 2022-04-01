using UnityEngine;
using UnityEngine.Events;

public class FinishCredits : MonoBehaviour
{
    public Animator PanelAnimator;
    public GameObject CreditsPanel;
    public UnityEvent<object> OnFinishCredits;

    private void Start()
    {
        OnFinishCredits.AddListener((object sender) => {
            Debug.Log($"Event: 'OnFinishCredits'; Sender: {sender}; Receiver: {this}");
        });
    }
    // Update is called once per frame
    private void Update()
    {
        if (PanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            OnFinishCredits.Invoke(this);
        }

        if (Input.anyKeyDown)
        {
            OnFinishCredits.Invoke(this);
        }

    }
}
