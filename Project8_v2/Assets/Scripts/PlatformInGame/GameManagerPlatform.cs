using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Both Model and viewer class in MVC
public class GameManagerPlatform : MonoBehaviour {

    // Game Manager Delegates
    public delegate void GameManageDelegate();
    public static event GameManageDelegate OnPauseGame;
    public static event GameManageDelegate OnUnpauseGame;
	public static event GameManageDelegate OnPlayerDied;
	//public static event GameManageDelegate OnGameOver;

    public static GameManagerPlatform instance;
    enum GameState{ Playing, Paused, GameOver, Respawn, Cutscene }
    GameState gameState = GameState.Playing;
    // Game State UI:s
    public GameObject playingUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject respawnUI;
    public GameObject cutsceneUI;

    // Score text and health text
    public Text textScore;
	public Text textHealth;
	public Text textLives;


	// Settings
	public int respawnDelay = 2;

    // Variables
    int score = 0;
	int maxHealth = 100;
	int health = 100;
	int lives = 3;
	public GameObject currentCheckpoint;

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
		//PlayerControllerPlatform.OnPlayerHeal += OnPlayerHeal;
		//PlayerControllerPlatform.OnPlayerScored += OnPlayerScored;
		ObjectStats.OnPlayerHeal += OnPlayerHeal;
		ObjectStats.OnPlayerHurt += OnPlayerHurt;
		ObjectStats.OnPlayerScored += OnPlayerScored;
		Checkpoint.SetCheckpoint += SetCheckpoint;
		PlayerControllerPlatform.OnPlayerPressPause += OnPlayerPressPause;
	}

	void OnDisable()
	{
		// Subscribe all listeners
		//PlayerControllerPlatform.OnPlayerHeal -= OnPlayerHeal;
		//PlayerControllerPlatform.OnPlayerScored -= OnPlayerScored;
		ObjectStats.OnPlayerHeal -= OnPlayerHeal;
		ObjectStats.OnPlayerHurt -= OnPlayerHurt;
		ObjectStats.OnPlayerScored -= OnPlayerScored;
		Checkpoint.SetCheckpoint -= SetCheckpoint;
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

	void SetCheckpoint (GameObject checkpoint)
	{
		currentCheckpoint = checkpoint;
	}


    void OnPlayerScored(int a)
    {
        score += a;
        textScore.text = "Score: " + score.ToString();
    }

	void OnPlayerHeal(int a)
	{
		if (health + a < maxHealth) {
			health += a;
		} else {
			health = maxHealth;
		}
		textHealth.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
	}

	void OnPlayerHurt(int a)
	{
		if (health - a > 0) {
			health -= a;
		} else {
			health = 0;
			Debug.Log ("Player has run out of HP!");
			lives--;
			textLives.text = "Lives: x" + lives.ToString ();
			if (lives == 0) {
				GameOver ();
			} else {
				OnPlayerDied ();
				health = maxHealth;
			}
		}
		textHealth.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
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

	void GameOver () {
		setGameState (GameState.GameOver);
	}
}