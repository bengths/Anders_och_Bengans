using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPlatform : MonoBehaviour {

    GameManagerPlatform game;

    void Start()
    {
        game = GameManagerPlatform.instance;
    }
}
