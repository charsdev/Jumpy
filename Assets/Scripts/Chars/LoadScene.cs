using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void DoChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
