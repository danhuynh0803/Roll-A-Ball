using UnityEngine;
using System.Collections;

public class KillBorder : MonoBehaviour {

	private LevelController levelController; 
	private bool isHit = false;
	void Start() {
		levelController = FindObjectOfType<LevelController> ();
	}

	void OnTriggerEnter(Collider other) { 
		if ( (other.tag == "Player1" || other.tag == "Player2") && !isHit) { 	
			isHit = true;
			ScoreController.decrementP1Score (levelController.fallValue);
			levelController.Respawn ();
		}
	}

	void OnTriggerExit(Collider other) { 
		isHit = false;
	}
	
}
