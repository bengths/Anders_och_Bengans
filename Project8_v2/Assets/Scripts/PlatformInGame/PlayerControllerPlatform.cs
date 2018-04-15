using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPlatform : MonoBehaviour {

    // Model and viewer
    GameManagerPlatform game;

    // Delegates
    public delegate void PlayerDelegate();

    public static event PlayerDelegate OnPlayerDeath;
    public static event PlayerDelegate OnPlayerPressPause;

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
	private bool isPaused;
    private bool isAttacking = false;
    private bool underCooldown = false;

	// Particles
	public GameObject deathParticles;
	public GameObject spawnParticles;


    void Start()
    {
        game = GameManagerPlatform.instance;
        setCharacterStats(character);
		isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OnPlayerPressPause();
        if (!isPaused) {
            playerAttack();
            playerMove();
        }
    }

    void playerAttack()
    {
        if (!underCooldown)
        {
            isAttacking = Input.GetKeyDown(KeyCode.LeftControl);
            if (isAttacking) GetComponent<Animator>().SetTrigger("attack");
            underCooldown = isAttacking;
        }


    }

    void OnAttackOver()
    {
        isAttacking = false;
        underCooldown = false;
    }

    void OnEnable()
    {
        GameManagerPlatform.OnPauseGame += OnPauseGame;
        GameManagerPlatform.OnUnpauseGame += OnUnpauseGame;
		GameManagerPlatform.OnPlayerDied += OnPlayerDied;
		GameManagerPlatform.OnGameOver += OnGameOver;
    }

    
	void OnDisable()
	{
		GameManagerPlatform.OnPauseGame -= OnPauseGame;
		GameManagerPlatform.OnUnpauseGame -= OnUnpauseGame;
		GameManagerPlatform.OnPlayerDied -= OnPlayerDied;
		GameManagerPlatform.OnGameOver -= OnGameOver;
	}


    void OnPauseGame()
    {
		isPaused = true;
    }

    void OnUnpauseGame()
    {
		isPaused = false;
    }


	void OnPlayerDied() {
		RespawnPlayer();
	}

	void OnGameOver () {
		// Freeze player
		Instantiate (deathParticles, this.transform.position, this.transform.rotation);
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		isPaused = true;
		//this.enabled = false;
		this.GetComponent<Renderer>().enabled = false;
		this.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
		this.GetComponent<BoxCollider2D> ().enabled = false;

	}

	void RespawnPlayer () {
		StartCoroutine ("RespawnPlayerCo");
	}

	IEnumerator RespawnPlayerCo () {
		Instantiate (deathParticles, this.transform.position, this.transform.rotation);
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		this.enabled = false;
		this.GetComponent<Renderer>().enabled = false;
		this.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
		this.GetComponent<BoxCollider2D> ().enabled = false;

		yield return new WaitForSeconds (game.respawnDelay);
		Debug.Log ("Player respawn");
		this.transform.position = game.currentCheckpoint.transform.position;
		Instantiate (spawnParticles, game.currentCheckpoint.transform.position, game.currentCheckpoint.transform.rotation);
		this.enabled = true;
		this.GetComponent<Renderer>().enabled = true;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		this.GetComponent<Rigidbody2D> ().gravityScale = 2.0f;

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
                playerJumpHeight = 10;
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
		GetComponent<Animator>().SetBool("isGrounded", isGrounded);
		if(isGrounded)
        {
			GetComponent<Animator>().SetBool("isJumping", false);
            doubleJumped = false;
            trippleJumped = false;
        }


        // Jump
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded&&!underCooldown)
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
        if (underCooldown) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * 0.1f * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        } else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

    }
		
    void jump()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, playerJumpHeight);
    }

	//void OnCollisionEnter2D (Collision2D trig) {
	//	Debug.Log ("Player has collided with " + trig.collider.name);
	//	if (trig.gameObject.tag == "Baesk") {
	//		OnPlayerScored (10);
	//		Destroy (trig.gameObject);
	//	}
	//}


}
