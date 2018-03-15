using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour {


    // Enemy stats
    public double maxHealth;
    public float attackCooldown;
    private double health;
    private Transform enemyTransform;
    // Use this for initialization
    void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Virtual functions
    public abstract void attack();
    public abstract void death();
}
