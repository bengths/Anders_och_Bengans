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

    void Start()
    {
        game = GameManagerPlatform.instance;
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

    void OnPauseGame()
    {
        ;
    }

    void OnUnpauseGame()
    {
        ;
    }
}
