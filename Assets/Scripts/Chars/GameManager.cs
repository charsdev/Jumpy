using Chars.Tools;
using Jumpy;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum Status { BeforeGameStart, GameInProgress, GameOver }
    public Status CurrentStatus;
    public bool GameIsPaused;
    public bool IsInputEnabled = true;
    public Image PausePanel;
    [HideInInspector] public GameObject player;
    [SerializeField] public GameObject PlayerPrefab;
    public bool CreatePlayer;
    public int Score;
    public bool canPause;

    protected override void Awake()
    {
        base.Awake();
        if (!CreatePlayer)
        {
            player = GameObject.FindWithTag("Player");
        }
        Cursor.lockState = CursorLockMode.None;
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
            SetGameState(Status.GameInProgress);
        }

        if (CurrentStatus == Status.GameInProgress)
        {
            //SetInput(true);

            if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && canPause)
            {
                PauseGame();
            }
        }

        //if (CurrentStatus == Status.GameOver)
        //{
        //    GameOver();
        //}
    }

    public void SetInput(bool value)
    {
        IsInputEnabled = value;
    }

    public void PauseGame()
    {
        GameIsPaused = !GameIsPaused;
        Time.timeScale = GameIsPaused ? 0 : 1;

        //TODO CHANGE
        PausePanel?.gameObject.SetActive(!PausePanel.gameObject.activeSelf);
    }

    public void GameOver()
    {
        Time.timeScale = 1;
    }

    public void OnEnable()
    {
        EventManager.StartListening("AddScore", AddScoreHandler);
    }

    public void OnDisable()
    {
        EventManager.StopListening("AddScore", AddScoreHandler);
    }

    public void AddScoreHandler(object sender, EventArgs eventparams)
    {
        Debug.Log($"Event: 'AddScore'; Sender: {sender}; Receiver: {this}");
        Score += ((AddScoreParams)eventparams).Value;
        UIManager.Instance.RefreshPoints();
    }


}
