using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPlatform : MonoBehaviour {

    // Model and viewer
    GameManagerPlatform game;

    // Delegates
    public delegate void PlayerDelegate();
    public delegate void PlayerDelegateInt(int a);

    public static event PlayerDelegate OnPlayerDeath;
    public static event PlayerDelegate OnPlayerPressPause;

    public static event PlayerDelegateInt OnPlayerScored;
    public static event PlayerDelegateInt OnPlayerHurt;
    public static event PlayerDelegateInt OnPlayerHeal;

    // Characters
    public enum playerCharacter{Anders, Anton, Dick, Johan, Jonas, Magnus, Marcus };
    public playerCharacter character;

    void Start()
    {
        game = GameManagerPlatform.instance;
        setCharacterStats(character);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OnPlayerPressPause();
    }

    void OnEnable()
    {
        GameManagerPlatform.OnPauseGame += OnPauseGame;
        GameManagerPlatform.OnUnpauseGame += OnUnpauseGame;
    }

	void OnDisable()
	{
		GameManagerPlatform.OnPauseGame -= OnPauseGame;
		GameManagerPlatform.OnUnpauseGame -= OnUnpauseGame;
	}


    void OnPauseGame()
    {
        ;
    }

    void OnUnpauseGame()
    {
        ;
    }

    void setCharacterStats(playerCharacter hero)
    {
        ;
    }
}
