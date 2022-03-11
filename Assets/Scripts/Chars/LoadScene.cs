using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void DoChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DoChangeSceneString(string level)
    {
        SceneManager.LoadScene(level);
    }
}
