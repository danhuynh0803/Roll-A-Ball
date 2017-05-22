using UnityEngine;
using System.Collections;

public class PickupController2 : MonoBehaviour {

	public float destroyTimer;								// Time till object is destroyed if not picked up
	bool isHit = false;
	LevelController2 levelController; 

	private AudioSource audio;
	void Start () {
		levelController = FindObjectOfType<LevelController2> ();	
		audio = GetComponent<AudioSource> ();
		Destroy (this.gameObject, destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

		// Remove all normal pickups during overtime
		if (this.tag == "Pickup" && levelController.hadOvertime) { 
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) 
	{		
		if (other.tag == "Player1" && isHit == false) { 
			audio.Play ();
			if (this.tag == "Pickup") { 
				ScoreController2.incrementP1Score (levelController.pointValue);
			} else if (this.tag == "Slowdown") { 
				other.gameObject.GetComponent<PlayerController1> ().SlowDown ();
			} else if (this.tag == "Speedup") { 
				other.gameObject.GetComponent<PlayerController1> ().SpeedUp ();
			}
			isHit = true;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			Destroy (this.gameObject, 3.0f);

		} 

		else if (other.tag == "Player2" && isHit == false) { 
			audio.Play ();
			if (this.tag == "Pickup") { 
				ScoreController2.incrementP2Score (levelController.pointValue);
			} else if (this.tag == "Slowdown") { 
				other.gameObject.GetComponent<PlayerController2> ().SlowDown ();
			} else if (this.tag == "Speedup") { 
				other.gameObject.GetComponent<PlayerController2> ().SpeedUp ();
			}
			isHit = true;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			Destroy (this.gameObject, 3.0f);
		}
	}
}
