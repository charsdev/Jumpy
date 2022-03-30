using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class StartGame : MonoBehaviour
{
    public UnityEvent<object> OnStartGame;
    public string FirtsLevel;
    public float TimeToStart;

    public void Play()
    {
        OnStartGame.AddListener((object sender) => {
            Debug.Log($"Event: 'OnStartGame'; Sender: {sender}; Receiver: {this}");
        });

        OnStartGame.Invoke(this);
        StartCoroutine(DelayPlay());
    }

    public IEnumerator DelayPlay()
    {
        yield return new WaitForSeconds(TimeToStart);
        SceneManager.LoadScene(FirtsLevel);
    }
}


