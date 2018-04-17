using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerFF : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;

	public static GameManagerFF Instance;

	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countdownPage;
	public Text scoreText;

	enum PageState{
		None,
		Start,
		GameOver,
		CountDown
	}

	int score = 0;
	bool gameOver = true;

	public bool GameOver {get { return gameOver; } }
	public int Score {get { return score; }}

	public AudioSource soundtrack;

	void OnCreate() {
		soundtrack.Play();
	}

	void Awake() {
		Instance = this;
	}

	void OnEnable() {
		CountDownText.OnCountdownFinished += OnCountdownFinished;
		PlayerController.OnPlayerDied += OnPlayerDied;
		PlayerController.OnPlayerScored += OnPlayerScored;
	}
		

	void OnDisable() {
		CountDownText.OnCountdownFinished -= OnCountdownFinished;
		PlayerController.OnPlayerDied -= OnPlayerDied;
		PlayerController.OnPlayerScored -= OnPlayerScored;
	}

	void OnCountdownFinished() {
		SetPageState (PageState.None);
		OnGameStarted (); // event sent to PlayerController
		score = 0;
		gameOver = false;
	}

	void OnPlayerDied() {
		gameOver = true;
		soundtrack.pitch = -0.6f;
		int savedScore = PlayerPrefs.GetInt ("HighScore");
		if (score > savedScore) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		SetPageState (PageState.GameOver);
	}

	void OnPlayerScored() {
		score++;
		scoreText.text = score.ToString();
		soundtrack.pitch = soundtrack.pitch + 0.01f;
	}



	void SetPageState(PageState state) {
		switch (state) {
			case PageState.None:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);	
				break;
			case PageState.Start:
				startPage.SetActive(true);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				break;
			case PageState.GameOver:
				startPage.SetActive(false);
				gameOverPage.SetActive(true);
				countdownPage.SetActive(false);
				break;
			case PageState.CountDown:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(true);
				break;
		}
	}

	public void ConfirmGameOver() {
		// activated when replay button is hit
		OnGameOverConfirmed(); // event sent to PlayerController
		scoreText.text = "0";
		soundtrack.pitch = 1.0f;
		SetPageState (PageState.Start);
	}

	public void StartGame() {
		// activated when play button is hit
		SetPageState(PageState.CountDown);
	}
}
