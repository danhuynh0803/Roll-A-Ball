using UnityEngine;
using System.Collections;

public class KillBorder2 : MonoBehaviour {

	private LevelController2 levelController; 
	private bool isHit = false;
	private bool isHit2 = false; 
	void Start() {
		levelController = FindObjectOfType<LevelController2> ();
	}

	void OnTriggerEnter(Collider other) { 
		if ((other.tag == "Player1" && !isHit)) { 	
			isHit = true;
			ScoreController2.decrementP1Score (levelController.fallValue);
			levelController.Respawn ();
		}
		
		if ((other.tag == "Player2" && !isHit2)) { 
			isHit2 = true;
			ScoreController2.decrementP2Score(levelController.fallValue);
			levelController.Respawn2();
		}
	}

	void OnTriggerExit(Collider other) { 
		if (other.tag == "Player1") {
			isHit = false;
		} else if (other.tag == "Player2") { 
			isHit2 = false;
		}
	}
	
}
