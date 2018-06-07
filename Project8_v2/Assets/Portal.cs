using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject player;
    public GameObject portalOut;
    private bool isPlayerAtDoor;

    public bool enterBossRoom;
    public AudioSource bossTheme;
    public AudioSource laughter;

    private void Start()
    {
        isPlayerAtDoor = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isPlayerAtDoor)
        {
            // Teleport player portal out
            player.GetComponent<Transform>().position = portalOut.GetComponent<Transform>().position;
            if (enterBossRoom)
            {
                // Play boss track
                GameManagerPlatform game = FindObjectOfType<GameManagerPlatform>();
                game.soundtrack.Pause();
                bossTheme.Play();
                laughter.Play();

                game.GetComponent<CameraSystem>().enabled = false;
                game.GetComponent<Transform>().position = new Vector3(152.1f, 31.4f, -10.0f);
                game.GetComponent<Camera>().orthographicSize = 8.9f;
                // Spawn boss
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerAtDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerAtDoor = false;
        }
    }

}
