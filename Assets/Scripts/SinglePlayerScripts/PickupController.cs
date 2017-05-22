using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

	public float destroyTimer;								// Time till object is destroyed if not picked up
	bool isHit = false;
	LevelController levelController; 

	private AudioSource audio;
	void Start () {
		levelController = FindObjectOfType<LevelController> ();	
		audio = GetComponent<AudioSource> ();
		Destroy (this.gameObject, destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{		
		if (other.tag == "Player1" && isHit == false) { 
			audio.Play ();
			if (this.tag == "Pickup") { 
				ScoreController.incrementP1Score (levelController.pointValue);
			} else if (this.tag == "Slowdown") { 
				other.gameObject.GetComponent<PlayerController> ().SlowDown ();
			} else if (this.tag == "Speedup") { 
				other.gameObject.GetComponent<PlayerController> ().SpeedUp ();
			}
			isHit = true;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			Destroy (this.gameObject, 3.0f);

		} 
			
	}
}
