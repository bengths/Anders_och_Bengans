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

	// Use this for initialization
	void Start () {
        state = nisseState.Walking;
        moveX = startDirection;
        setCharacterStats(enemyType);
	}
	
	// Update is called once per frame
	void Update () {
        if (state == nisseState.Walking)
            EnemyMove();

        // Check for collisions
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(moveX, 0));
        if (hit.distance < 3.0f)
            if (hit.collider.tag == "ground")
                moveX *= -1;
    }

    void setCharacterStats(enemyCharacter enemy)
    {
        switch (enemy)
        {
            case enemyCharacter.Nisse:
                walkSpeed = 4.0f;
                break;
            case enemyCharacter.Olle:
                walkSpeed = 11.0f;
                break;
        }
    }

    public void EnemyMove()
    {
        // Animations
        Debug.Log((moveX!=0.0f));
        GetComponent<Animator>().SetBool("isWalking", (moveX != 0.0f)); // Idle/Walking
        if (moveX != 0.0f) GetComponent<SpriteRenderer>().flipX = (moveX > 0.0f); // Direction

        // Kinematics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * walkSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    // Override virtual functions
    public override void attack()
    {
        ;
    }

    public override void death() {
        ;
    }
}
