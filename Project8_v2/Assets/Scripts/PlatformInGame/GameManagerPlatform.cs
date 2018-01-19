using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Both Model and viewer class in MVC
public class GameManagerPlatform : MonoBehaviour {

    public static GameManagerPlatform instance;
    enum GameState{ Playing, Paused, GameOver, Respawn, Cutscene }
    // Game State UI:s
    public GameObject playingUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject respawnUI;
    public GameObject cutsceneUI;

    // Private functions
    void Awake()
    {
        instance = this;
    }

    void setAllStatesFalseUI()
    {
        playingUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        respawnUI.SetActive(false);
        cutsceneUI.SetActive(false);
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
}