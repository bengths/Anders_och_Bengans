using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Enemy behaviour for Nisse(Non-flying interactive non-static sassy enemies)
 * States : Idle, Walking, Attack, Dying
 */
public class EnemyNisse : EnemyClass {

    public float startDirection = 1;
    public enum enemyCharacter {Nisse, Olle};
    private enum nisseState {Idle, Walking, Attack, Dying};
    private nisseState state;
    public enemyCharacter enemyType;
    private float moveX;
    private float walkSpeed;
    private float attackDistance;
    private bool canAttack = true;
    public GameObject damageTrigger;

	private int healthPoints;
	// Particles
	public GameObject deathParticles;

	// Audio
	public AudioSource hitSound;
	public AudioSource woofSound;
	public AudioSource popSound;

    // Projectile
    public GameObject projectileHorizontal;

    // Use this for initialization
    void Start () {
        state = nisseState.Walking;
        moveX = startDirection;
        setCharacterStats(enemyType);
	}

	// Update is called once per frame
	void Update () {
		if (state != nisseState.Dying) {
			if (state == nisseState.Walking) {
				EnemyMove ();
			}

			// Check for collisions
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (moveX, 0));
			if (hit.distance < 3.0f) {
				if (hit.collider.tag == "ground") {
					moveX *= -1;
				}
			}
			// Check for player
			if (hit.distance < this.attackDistance) {
				if (hit.collider.tag == "Player" && canAttack) {
					attack ();
				}
			}
		}
    }

    void setCharacterStats(enemyCharacter enemy)
    {
        switch (enemy)
        {
			case enemyCharacter.Nisse:
				walkSpeed = 3.0f;
				attackDistance = 10.0f;
				attackCooldown = 3.0f;
				healthPoints = 30;
                break;
            case enemyCharacter.Olle:
				walkSpeed = 5.0f;
				attackDistance = 10.0f;
				attackCooldown = 5.0f;
				healthPoints = 40;
				break;
        }
    }

    public void EnemyMove()
    {
        // Animations
        GetComponent<Animator>().SetBool("isWalking", (moveX != 0.0f)); // Idle/Walking
        if (moveX != 0.0f) GetComponent<SpriteRenderer>().flipX = (moveX > 0.0f); // Direction

        // Kinematics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * walkSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

	// Enemy Health Management

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "PlayerAttackTrigger" && state != nisseState.Dying) {
			enemyHurt(col.gameObject.GetComponent<attackTrigger>().getAttackPoints());
		}
	}

	private void enemyHurt(int atkPoints) {
		healthPoints -= atkPoints;
		Debug.Log ("Nisse HP = " + healthPoints);
		hitSound.Play ();
		if (healthPoints <= 0) {
			death ();
		}
	}


    private void Event_NisseAttackOver()
    {
        // Create projectile
        GameObject missile = Instantiate(projectileHorizontal, this.transform.position, this.transform.rotation);
        missile.GetComponent<nisse_fireball>().setHorz_dir(moveX);

        // Update animation and reset state
        GetComponent<Animator>().SetBool("attack", false);
        this.state = nisseState.Walking;

        // Cooldown timer for next attack
        Debug.Log("timer start");
        StartCoroutine("AttackCooldownCo");
    }

    IEnumerator AttackCooldownCo()
    {
        yield return new WaitForSeconds((0.3f + Random.Range(0, attackCooldown)));
        Debug.Log("timer done");
        canAttack = true;
    }


    // Override virtual functions
    public override void attack()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        this.state = nisseState.Attack;
        GetComponent<Animator>().SetBool("attack", true);
        canAttack = false;
    }

    public override void death() {
		this.state = nisseState.Dying;
		woofSound.Play ();
		canAttack = false;	// Enemy can't attack anymore
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f); // Enemy stops moving
		damageTrigger.SetActive(false);
		//gameObject.GetComponentInChildren<ObjectStats>().enabled = false; // Enemy touch won't hurt player anymore
		GetComponent<Animator> ().SetBool ("isDying", true);
		StartCoroutine("waitForDeathAnimationCo");
    }

	IEnumerator waitForDeathAnimationCo()
	{
		yield return new WaitForSeconds(4.0f);
		popSound.Play ();
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		Instantiate (deathParticles, this.transform.position, this.transform.rotation);

	}

	IEnumerator waitToDestroy(){
		yield return new WaitForSeconds (2.0f);
		Destroy (this.gameObject);
	}
}
