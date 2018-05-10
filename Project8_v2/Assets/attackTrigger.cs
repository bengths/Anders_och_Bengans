using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

	private int playerAttackPoints;

	public void setAttackPoints(int atkPts){
		playerAttackPoints = atkPts;
	}

	public int getAttackPoints() {
		return playerAttackPoints;
	}


}
