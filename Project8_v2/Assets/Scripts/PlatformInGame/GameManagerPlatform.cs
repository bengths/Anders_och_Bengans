﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Both Model and viewer class in MVC
public class GameManagerPlatform : MonoBehaviour {

    // Game Manager Delegates
    public delegate void GameManageDelegate();
    public static event GameManageDelegate OnPauseGame;
    public static event GameManageDelegate OnUnpauseGame;
	public static event GameManageDelegate OnPlayerDied;
	public static event GameManageDelegate OnGameOver;
	//public static event GameManageDelegate OnReplay;

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
    public Text textLevel;
    public GameObject healthBar;

    // Settings
    public int respawnDelay = 2;

    // Variables
    int score = 0;
	int maxHealth = 100;
	int health = 100;
	int lives = 3;
	public GameObject startingCheckpoint;
	public GameObject currentCheckpoint;

	// Audio
	public AudioSource soundtrack;
	public AudioSource scoreSound;
	public AudioSource hurtSound;
	public AudioSource extraLifeSound;
	public AudioSource healSound;
	public AudioSource deathSound;


    // Public functions
    public int getScore() {
        return score;
    }

    public bool isGameOver() {
        return gameState == GameState.GameOver;
    }

	// Button functions and onEvent functions
    public void pressResumeButton() {
        OnUnpauseGame();
        setGameState(GameState.Playing);
		Time.timeScale = 1;
    }

	public void pressReplayButton()	{
		Time.timeScale = 1;
		soundtrack.pitch = 1.0f;
		SceneManager.LoadScene ("Demo_Scene");		// Must be dynamic, load current scene instead!!!
		setGameState (GameState.Playing);
	}


	void OnPlayerScored(int a) {
		scoreSound.Play ();
		score += a;
		setScoreText ();
	}

	void OnPlayerHeal(int a) {
		healSound.Play ();
		if (health + a < maxHealth) {
			health += a;
		} else {
			health = maxHealth;
		}
		setHealthText ();
	}

	void OnPlayerHurt(int a) {
		if (health - a > 0) {
			hurtSound.Play ();
			health -= a;
		} else {
			deathSound.Play ();
			health = 0;
			Debug.Log ("Player has run out of HP!");
			lives--;
			setLifeText ();
			if (lives <= 0) {
				setGameState (GameState.GameOver);
				setCheckpoint (startingCheckpoint);
				OnGameOver ();	// Send event
				soundtrack.pitch = 0.7f;
				lives = 0;
				setLifeText ();
				Time.timeScale = 0.1f;
			} else {
				OnPlayerDied ();
				health = maxHealth;
			}
		}
		setHealthText ();
	}

	void OnPlayerExtraLife() {
		extraLifeSound.Play ();
		lives++;
		setLifeText ();
	}
		
	void OnPlayerPressPause() {
		// Pause game
		if (gameState == GameState.Playing) {
			OnPauseGame();
			setGameState(GameState.Paused);
			Time.timeScale = 0;
			return;
		}
		// Unpause game
		if (gameState == GameState.Paused) {
			OnUnpauseGame ();
			setGameState (GameState.Playing);
			Time.timeScale = 1;
		}
	}


    // Private functions
	void Start() {
		setCheckpoint(startingCheckpoint);
		Time.timeScale = 1;
        textLevel.text = PlayerPrefs.GetString("CurrentLevel");
    }

    void Awake() {
        instance = this;
    }

    void OnCreate() {
        // Initiate
		Time.timeScale = 1.0f;
		soundtrack.Play();
		setHealthText ();
		setLifeText ();
		setScoreText ();

    }

	void OnEnable() {
		// Subscribe all listeners
		//PlayerControllerPlatform.OnPlayerHeal += OnPlayerHeal;
		//PlayerControllerPlatform.OnPlayerScored += OnPlayerScored;
		ObjectStats.OnPlayerHeal += OnPlayerHeal;
		ObjectStats.OnPlayerHurt += OnPlayerHurt;
		ObjectStats.OnPlayerScored += OnPlayerScored;
		ObjectStats.OnPlayerExtraLife += OnPlayerExtraLife;
		Checkpoint.setCheckpoint += setCheckpoint;
		PlayerControllerPlatform.OnPlayerPressPause += OnPlayerPressPause;
		//PlayerControllerPlatform.OnPlayerAttack += OnPlayerAttack;
	}

	void OnDisable() {
		// Subscribe all listeners
		//PlayerControllerPlatform.OnPlayerHeal -= OnPlayerHeal;
		//PlayerControllerPlatform.OnPlayerScored -= OnPlayerScored;
		ObjectStats.OnPlayerHeal -= OnPlayerHeal;
		ObjectStats.OnPlayerHurt -= OnPlayerHurt;
		ObjectStats.OnPlayerScored -= OnPlayerScored;
		ObjectStats.OnPlayerExtraLife -= OnPlayerExtraLife;
		Checkpoint.setCheckpoint -= setCheckpoint;
		PlayerControllerPlatform.OnPlayerPressPause -= OnPlayerPressPause;
		//PlayerControllerPlatform.OnPlayerAttack -= OnPlayerAttack;
	}

    void setAllStatesFalseUI() {
        playingUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        respawnUI.SetActive(false);
        cutsceneUI.SetActive(false);
    }

    void setGameState(GameState state) {
        gameState = state;
        setUIState(state);
    }

    void setUIState(GameState state) {
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

	void setCheckpoint (GameObject checkpoint) {
		currentCheckpoint = checkpoint;
	}

	void setScoreText () {
		textScore.text = "Score: " + score.ToString();
	}

	void setHealthText () {
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(health, maxHealth);
		textHealth.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
	}

	void setLifeText() {
		textLives.text = "Lives: x" + lives.ToString ();
	}

	//void OnPlayerAttack (float a, int b)
	//{
		// The event sent from PlayerControllerPlatform requires a listener.
		// This is a dummy listener
	//}

}