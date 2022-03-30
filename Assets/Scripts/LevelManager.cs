using Chars.Tools;
using Jumpy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public CheckPoint CurrentCheckPoint;
    public float IntroFadeDuration = 1f;
    public float OutroFadeDuration = 2f;
    public float RespawnDelay = 2f;
    private string _nextLevel;
    public List<CheckPoint> Checkpoints;

    protected override void Awake()
    {
        InstantiatePlayer();
        InitializeCheckpoints();
        SpawnPlayer();
    }

    protected virtual void Start()
    {
        EventManager.TriggerEvent("FadeOut", new FadeEventParams(OutroFadeDuration), this);
    }

    public void SpawnPlayer()
    {
        CurrentCheckPoint.Spawn(GameManager.instance.player);
    }

    private void InitializeCheckpoints()
    {
        CurrentCheckPoint = Checkpoints.Count > 0 ? Checkpoints[0] : null;
    }

    private void InstantiatePlayer()
    {
        if (GameManager.Instance.PlayerPrefab != null && GameManager.instance.CreatePlayer)
        {
            GameObject newPlayer = Instantiate(GameManager.Instance.PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameManager.instance.player = newPlayer;
            return;
        }
    }

    public virtual void SetCurrentCheckpoint(CheckPoint newCheckPoint)
    {
        CurrentCheckPoint = newCheckPoint;
    }

    public virtual void SetNextLevel(string levelName)
    {
        _nextLevel = levelName;
    }

    public virtual void GotoNextLevel()
    {
        GotoLevel(_nextLevel);
        _nextLevel = null;
    }

    public virtual void GotoLevel(string levelName)
    {
        EventManager.TriggerEvent("FadeIn", new FadeEventParams(OutroFadeDuration), this);
        StartCoroutine(GotoLevelCo(levelName));
    }

    public virtual void RestartLevel()
    {
        EventManager.TriggerEvent("FadeIn", new FadeEventParams(OutroFadeDuration), this);
        var activeScene = SceneManager.GetActiveScene();
        StartCoroutine(GotoLevelCo(activeScene.name));
    }

    protected virtual IEnumerator GotoLevelCo(string levelName)
    {
       
        if (Time.timeScale > 0.0f)
        {
            yield return new WaitForSeconds(OutroFadeDuration);
        }
        else
        {
            yield return new WaitForSecondsRealtime(OutroFadeDuration);
        }

        if (string.IsNullOrEmpty(levelName))
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("GameOver", GameOverHandler);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", GameOverHandler);
    }

    public void GameOverHandler(object sender, EventArgs eventParams)
    {
        Debug.Log($"Event: 'GameOver'; Sender: {sender}; Receiver: {this}");
        GotoNextLevel();
    }

}
