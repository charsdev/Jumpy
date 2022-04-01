using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class QuitApplication : MonoBehaviour 
{
	public UnityEvent<object> OnQuit;

	public float TimeToExit;

    public void Quit()
	{
		OnQuit.AddListener((object sender) => {
			Debug.Log($"Event: 'OnQuit'; Sender: {sender}; Receiver: {this}");
		});

		OnQuit.Invoke(this);
		StartCoroutine(QuitCO());
	}

    public IEnumerator QuitCO()
    {
		yield return new WaitForSeconds(TimeToExit);
		Debug.Log("exit");

		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
		//Quit the application
			Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
