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

    // Set character attributes to default
    private int playerSpeed = 10;
    private float playerJumpHeight = 6.0f;
    private bool canDoubleJump = false;
    private bool canTrippleJump = false;

    // Movement variables
    private float moveX;
    public Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool doubleJumped;
    private bool trippleJumped;


    void Start()
    {
        game = GameManagerPlatform.instance;
        setCharacterStats(character);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OnPlayerPressPause();
        else playerMove(); 
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
        switch(hero)
        {
            case playerCharacter.Dick:
                playerJumpHeight = 6;
                playerSpeed = 7;
                canDoubleJump = true;
                break;
            case playerCharacter.Magnus:
                playerJumpHeight = 8;
                playerSpeed = 8;
                break;
        }
    }

    void playerMove()
    {
        // ´Horizontal controls
        moveX = Input.GetAxis("Horizontal");
        
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if(isGrounded)
        {
            doubleJumped = false;
            trippleJumped = false;
        }


        // Jump
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded)
            {
                jump();
                GetComponent<Animator>().SetBool("isJumping", true);
            } else
            {
                if ((!doubleJumped && canDoubleJump)|| (!trippleJumped && canTrippleJump))
                {
                    jump();
                    trippleJumped = doubleJumped;
                    doubleJumped = true;
                }
            }
        }

        // Animations
        GetComponent<Animator>().SetBool("isWalking", moveX != 0.0f); // Idle/Walking
        if(moveX != 0.0f) GetComponent<SpriteRenderer>().flipX = (moveX < 0.0f); // Direction

        // Kinematics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void jump()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, playerJumpHeight);
    }

}
