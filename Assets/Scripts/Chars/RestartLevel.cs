using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart()
    {
       Time.timeScale = 1;
       var activeScene = SceneManager.GetActiveScene();
       SceneManager.LoadScene(activeScene.name);
    }
}
