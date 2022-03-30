using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShowCredits : MonoBehaviour
{
    public UnityEvent<object> OnShowCredits;
    public void Show()
    {
        OnShowCredits.AddListener((object sender) => {
            Debug.Log($"Event: 'OnStartGame'; Sender: {sender}; Receiver: {this}");
        });

        OnShowCredits.Invoke(this);
    }
}
