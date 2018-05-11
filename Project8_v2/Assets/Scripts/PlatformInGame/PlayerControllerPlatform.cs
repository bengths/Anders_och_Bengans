using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPlatform : MonoBehaviour {

    // Model and viewer
    GameManagerPlatform game;

    // Delegates
    public delegate void PlayerDelegate();
	public delegate void PlayerDelegateInt(float a,int b);

    public static event PlayerDelegate OnPlayerDeath;
    public static event PlayerDelegate OnPlayerPressPause;
	public static event PlayerDelegateInt OnPlayerAttack;

    // Characters
    public enum playerCharacter{Anders, Anton, Dick, Johan, Jonas, Magnus, Marcus };
    public playerCharacter character;

    // Set character attributes to default
    private int playerSpeed = 10;
    private float playerJumpHeight = 6.0f;
    private bool canDoubleJump = false;
    private bool canTrippleJump = false;
	private float playerAttackRange = 5.0f;
	private int playerAttackPoints = 1;
	private bool isShooter = false;

    // Movement variables
    private float moveX;
    public Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool doubleJumped;
    private bool trippleJumped;
	private bool isPaused;
   
	// Particles
	public GameObject deathParticles;
	public GameObject spawnParticles;
	public GameObject bullets;

	// Attack related
	public GameObject attackTrigger;
	private float attackTriggerShift;
	private bool isAttacking = false;
	private bool underCooldown = false;
	private bool isGunActive = false;
	private float shooterCooldown = 0.1f;
	public GameObject circleAttackTrigger;
	private bool forceFieldOn = false;
	private bool forceFieldToggle = false;
	private int counter = 0;

	// Audio
	public AudioSource shootSound;
	public AudioSource spawnSound;
	public AudioSource forceFieldSound;

    void Start()
    {
        game = GameManagerPlatform.instance;
        setCharacterStats(character);
		isPaused = false;
		attackTrigger.GetComponent<BoxCollider2D> ().enabled = false;
		circleAttackTrigger.SetActive (false);
		forceFieldSound.Play ();
		forceFieldSound.Pause ();

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
		spawnSound.Play ();
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
		case playerCharacter.Magnus:
			GetComponent<Animator> ().SetInteger ("Character", 1);
			canDoubleJump = false;
			canTrippleJump = false;
			isShooter = false;
			playerJumpHeight = 10;
			playerSpeed = 8;
			playerAttackRange = 5.0f;
			playerAttackPoints = 10;
			GetComponent<Transform> ().localScale = new Vector3 (2.0f, 2.0f, 2.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.9f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f,this.transform.position.z);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.4f, 0.3f);
			attackTriggerShift = 0.6f;
			break;
		case playerCharacter.Anders:
			GetComponent<Animator> ().SetInteger ("Character", 2);
			canDoubleJump = false;
			canTrippleJump = false;
			isShooter = false;
			playerJumpHeight = 10;
			playerSpeed = 6;
			playerAttackRange = 5.0f;
			playerAttackPoints = 15;
			GetComponent<Transform> ().localScale = new Vector3 (3.0f,3.0f,3.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.7f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f,this.transform.position.z);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.6f,0.5f);
			attackTriggerShift = 0.0f;
			break;
		case playerCharacter.Anton:
			GetComponent<Animator> ().SetInteger ("Character", 3);
			canDoubleJump = true;
			canTrippleJump = true;
			isShooter = false;
			playerJumpHeight = 7;
			playerSpeed = 8;
			playerAttackRange = 5.0f;
			playerAttackPoints = 10;
			GetComponent<Transform> ().localScale = new Vector3 (2.5f, 2.5f, 2.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.7f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f,this.transform.position.z);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.4f, 0.3f);
			attackTriggerShift = 0.6f;
			break;
		case playerCharacter.Johan:
			GetComponent<Animator> ().SetInteger ("Character", 4);
			canDoubleJump = false;
			canTrippleJump = false;
			isShooter = false;
			playerJumpHeight = 10;
			playerSpeed = 6;
			playerAttackRange = 5.0f;
			playerAttackPoints = 20;
			GetComponent<Transform> ().localScale = new Vector3 (2.5f, 2.5f, 2.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.7f);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.55f, 0.4f);
			attackTriggerShift = 0.6f;
			break;
		case playerCharacter.Jonas:
			GetComponent<Animator> ().SetInteger ("Character", 5);
			canDoubleJump = false;
			canTrippleJump = false;
			isShooter = true;
			playerJumpHeight = 10;
			playerSpeed = 7;
			playerAttackPoints = 1;
			shooterCooldown = 0.1f;
			isGunActive = true;
			GetComponent<Transform> ().localScale = new Vector3 (2.5f, 2.5f, 2.5f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.6f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 0.8f,this.transform.position.z);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.4f, 0.3f);
			attackTriggerShift = 0.9f;
			break;
		case playerCharacter.Marcus:
			GetComponent<Animator> ().SetInteger ("Character", 6);
			canDoubleJump = true;
			canTrippleJump = false;
			isShooter = false;
			playerJumpHeight = 10;
			playerSpeed = 8;
			playerAttackRange = 5.0f;
			playerAttackPoints = 2;
			GetComponent<Transform> ().localScale = new Vector3 (2.5f, 2.5f, 2.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.7f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f,this.transform.position.z);

			circleAttackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			break;
		case playerCharacter.Dick:
			GetComponent<Animator> ().SetInteger ("Character", 7);
			canDoubleJump = false;
			canTrippleJump = false;
			isShooter = true;
			isGunActive = false;
			playerJumpHeight = 10;
			playerSpeed = 8;
			playerAttackPoints = 12;
			shooterCooldown = 1.0f;
			GetComponent<Transform> ().localScale = new Vector3 (3.0f, 3.0f, 3.0f);
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.3f, 0.6f);
			groundCheck.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f, this.transform.position.z);

			attackTrigger.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
			attackTrigger.GetComponent<BoxCollider2D> ().size = new Vector2 (0.4f, 0.3f);
			attackTriggerShift = 0.9f;
			break;
		}
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OnPlayerPressPause();
        if (!isPaused) {
			playerMove();
			if (character != playerCharacter.Marcus) {
				playerAttack ();
			} else {
				passiveAttack ();
			}
        }
    }

    void playerAttack()
    {
		counter++;
		if (!underCooldown) {
			isAttacking = Input.GetKeyDown (KeyCode.LeftControl);
			underCooldown = isAttacking;
			if (isAttacking) {
				if (isShooter) {
					if (isGunActive) {
						shootSound.Play ();
						GameObject bullet = Instantiate (bullets, attackTrigger.GetComponent<Transform> ());
						bullet.GetComponent<attackTrigger> ().setAttackPoints (playerAttackPoints);
						bullet.GetComponent<bulletScript> ().setHorz_dir ((this.GetComponent<SpriteRenderer> ().flipX) ? -1 : 1);
						StartCoroutine ("ShooterCooldownCo");
					} else {
						OnAttackOver ();
					}
				} else {
					GetComponent<Animator> ().SetTrigger ("attack");
					attackTrigger.GetComponent<BoxCollider2D> ().enabled = true;
					counter = 0; 	// Reset counter
					StartCoroutine ("disableAttackTrigger");
					//OnPlayerAttack (playerAttackRange, playerAttackPoints); 	// Send Attack event
				}
			}
		}
    }

	IEnumerator disableAttackTrigger() {
		yield return new WaitUntil(() => (counter > 2));
		attackTrigger.GetComponent<BoxCollider2D> ().enabled = false;
	}

    void OnAttackOver()
    {
        isAttacking = false;
        underCooldown = false;
		attackTrigger.GetComponent<BoxCollider2D> ().enabled = false;
    }

	IEnumerator ShooterCooldownCo() {
		yield return new WaitForSeconds (shooterCooldown);
		isAttacking = false;
		underCooldown = false;
	}

	void passiveAttack() {
		if (forceFieldOn) {
			circleAttackTrigger.SetActive (true);
			//circleAttackTrigger.GetComponent<CircleCollider2D> ().enabled = false;
			counter++;
			if ((counter % 10) < 3) {
				circleAttackTrigger.GetComponent<CircleCollider2D> ().enabled = true;
			} else {
				circleAttackTrigger.GetComponent<CircleCollider2D> ().enabled = false;
			}
		} else {
			circleAttackTrigger.SetActive (false);
			counter = 0;
		}
	}

    void playerMove()
	{
		// Horizontal controls
		moveX = Input.GetAxis ("Horizontal");

		// Update attackTrigger transform
		attackTrigger.GetComponent<Transform> ().position = new Vector3 (this.GetComponent<Transform> ().position.x + attackTriggerShift * ((this.GetComponent<SpriteRenderer> ().flipX) ? -1 : 1), this.GetComponent<Transform> ().position.y, this.GetComponent<Transform> ().position.z);
        
		// Check if grounded
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		GetComponent<Animator> ().SetBool ("isGrounded", isGrounded);
		if (isGrounded) {
			GetComponent<Animator> ().SetBool ("isJumping", false);
			doubleJumped = false;
			trippleJumped = false;
		}

		// Jump
		if (Input.GetButtonDown ("Jump")) {
			if (isGrounded && !underCooldown) {
				jump ();
				GetComponent<Animator> ().SetBool ("isJumping", true);
			} else {
				if ((!doubleJumped && canDoubleJump) || (!trippleJumped && canTrippleJump)) {
					jump ();
					trippleJumped = doubleJumped;
					doubleJumped = true;
				}
			}
		}

		// Toggle Ready Gun (Dick only)
		if (Input.GetKeyDown (KeyCode.R) && character == playerCharacter.Dick){
			if (isGunActive) {
				isGunActive = false;
			} else {
				isGunActive = true;
			}
			GetComponent<Animator> ().SetBool ("isGunActive", isGunActive);
		}

		// Toggle Force Field (Marcus only)

		if (character == playerCharacter.Marcus) {
			if (Input.GetKeyDown (KeyCode.R)) {
				if (forceFieldToggle) {
					forceFieldToggle = false;
				} else {
					forceFieldToggle = true;
				}
			}

			// Set status of Force Field
			if (!isGrounded) {
				forceFieldOn = true;
				forceFieldSound.UnPause ();
				GetComponent<Animator> ().SetBool ("isGunActive", forceFieldOn);
			} else if (forceFieldToggle) {
				forceFieldOn = true;
				forceFieldSound.UnPause ();
				GetComponent<Animator> ().SetBool ("isGunActive", forceFieldOn);
			} else {
				forceFieldOn = false;
				forceFieldSound.Pause ();
				GetComponent<Animator> ().SetBool ("isGunActive", forceFieldOn);
			}
		}

        // Animations
        GetComponent<Animator>().SetBool("isWalking", moveX != 0.0f); // Idle/Walking
        if(moveX != 0.0f) GetComponent<SpriteRenderer>().flipX = (moveX < 0.0f); // Direction

		// Falling animation
		GetComponent<Animator> ().SetBool ("isFalling", GetComponent<Rigidbody2D> ().velocity.y < 0.0f);


        // Kinematics
        if (underCooldown) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * 0.1f * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        } else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

		if (isGunActive && character != playerCharacter.Jonas) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * 0.5f * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
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
