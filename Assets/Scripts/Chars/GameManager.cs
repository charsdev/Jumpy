using Chars.Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum Status { BeforeGameStart, GameInProgress, GameOver }
    public Status CurrentStatus;
    public bool GameIsPaused;
    public bool IsInputEnabled = true;
    public Image PausePanel;
    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Start()
    {
        SetGameState(Status.BeforeGameStart);
        Application.targetFrameRate = 300;
    }

    public void SetGameState(Status state) => CurrentStatus = state;

    private void Update()
    {
        if(CurrentStatus == Status.BeforeGameStart)
        {
            //DANGER DEPENDECY
            if (!FadeEffect.FadeFinish)
            {
                IsInputEnabled = false;
            }
            else
            {
                IsInputEnabled = true;
                SetGameState(Status.GameInProgress);
            }
        }
   
        if (CurrentStatus == Status.GameInProgress)
        {
            SetInput(true);

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameIsPaused = !GameIsPaused;
                PauseGame();
            }
        }

        if (CurrentStatus == Status.GameOver)
        {
            GameOver();
        }
    }

    private void SetInput(bool value)
    {
        IsInputEnabled = value;
    }

    private void PauseGame()
    {
        Time.timeScale = GameIsPaused ? 0 : 1;
        PausePanel.gameObject.SetActive(!PausePanel.gameObject.activeInHierarchy);
    }

    public void GameOver()
    {
        FadeEffect.Instance.FadeInToScene("Menu");
    }
}
