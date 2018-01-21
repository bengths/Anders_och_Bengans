﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Both Model and viewer class in MVC
public class GameManagerPlatform : MonoBehaviour {

    // Game Manager Delegates
    public delegate void GameManageDelegate();
    public static event GameManageDelegate OnPauseGame;
    public static event GameManageDelegate OnUnpauseGame;

    public static GameManagerPlatform instance;
    enum GameState{ Playing, Paused, GameOver, Respawn, Cutscene }
    GameState gameState = GameState.Playing;
    // Game State UI:s
    public GameObject playingUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject respawnUI;
    public GameObject cutsceneUI;

    // Score text
    public Text textScore;

    // Variables
    int score = 0;
    public AudioSource soundtrack;

    // Public functions
    public int getScore()
    {
        return score;
    }

    public bool isGameOver()
    {
        return gameState == GameState.GameOver;
    }

    public void pressResumeButton()
    {
        OnUnpauseGame();
        setGameState(GameState.Playing);
    }

    // Private functions
    void Awake()
    {
        instance = this;
    }

    void OnCreate()
    {
        soundtrack.Play();
    }

    void OnEnable()
    {
        // Subscribe all listeners
        PlayerControllerPlatform.OnPlayerScored += OnPlayerScored;
        PlayerControllerPlatform.OnPlayerPressPause += OnPlayerPressPause;
    }

	void OnDisable()
	{
		// Subscribe all listeners
		PlayerControllerPlatform.OnPlayerScored -= OnPlayerScored;
		PlayerControllerPlatform.OnPlayerPressPause -= OnPlayerPressPause;
	}

    void setAllStatesFalseUI()
    {
        playingUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        respawnUI.SetActive(false);
        cutsceneUI.SetActive(false);
    }

    void setGameState(GameState state)
    {
        gameState = state;
        setUIState(state);
    }

    void setUIState(GameState state)
    {
        switch(state)
        {
            case GameState.Playing:
                setAllStatesFalseUI();
                playingUI.SetActive(true);
                break;
            case GameState.Paused:
                setAllStatesFalseUI();
                pausedUI.SetActive(true);
                break;
            case GameState.GameOver:
                setAllStatesFalseUI();
                gameOverUI.SetActive(true);
                break;
            case GameState.Respawn:
                setAllStatesFalseUI();
                respawnUI.SetActive(true);
                break;
            case GameState.Cutscene:
                setAllStatesFalseUI();
                cutsceneUI.SetActive(true);
                break;
        }
    }

    void OnPlayerScored(int a)
    {
        score += a;
        textScore.text = "Score: " + score.ToString();
    }

    void OnPlayerPressPause()
    {
        // Pause game
        if (gameState == GameState.Playing) {
            OnPauseGame();
            setGameState(GameState.Paused);
            return;
        }
        // Unpause game
        if (gameState == GameState.Paused)
        {
            OnUnpauseGame();
            setGameState(GameState.Playing);
        }
    }
}