using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private bool canUseKey = false;
    [SerializeField] private string nextLevel;
    
    public void DoChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DoChangeSceneString(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void EnableKeys()
    {
        canUseKey = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && canUseKey)
        {
            DoChangeSceneString(nextLevel);
        }
    }
}
