using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour {

	public float speed;						// Movement speed
	public float jump;						// Jump force
	public float knockbackForce;			// Knockback loser in collision
	public float wallKnockbackForce; 

	private Rigidbody rb;
	private LevelController2 levelController;
	private Renderer render;


	private bool canJump = false;			// Initialize to false to avoid player jumping in air when game begins
	private float jumpForce = 0.0f;
	private bool hasPowerup = false;
	private float currPowerupTimer;
	private string powerup;

	void Start () {
		levelController = FindObjectOfType<LevelController2> ();
		rb = GetComponent<Rigidbody> ();
		render = GetComponent<Renderer> ();
		hasPowerup = false;
	}

	void Update() { 
		currPowerupTimer -= Time.deltaTime;
		if (currPowerupTimer > 0.0f) {
			if (powerup.Equals ("Speedup")) {
				render.material.color = new Color (0.3f, 1.0f, 0.3f, 1.0f);
				speed = 12.0f;
			} else if (powerup.Equals ("Slowdown")) { 
				render.material.color = new Color (1.0f, 0.3f, 0.3f, 1.0f);
				speed = 4.0f;
			}
		} 

		if (currPowerupTimer <= 0.0f) { 
			render.material.color = new Color (0.8f, 0.8f, 0.8f, 1.0f);
			speed = 8.0f;
			powerup = "";
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (canJump && Input.GetKeyDown ("space")) {
			jumpForce = jump;
			canJump = false;
		} 
		else {
			jumpForce = 0.0f;
		}

		Vector3 movement = new Vector3 (moveHorizontal, jumpForce, moveVertical);
		rb.AddForce (movement * speed);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Ground") { 
			canJump = true;
		}

		if (other.gameObject.tag == "Wall") { 
			ScoreController2.decrementP1Score (levelController.wallValue);
			Vector3 wallToP1 = this.gameObject.transform.position - other.gameObject.transform.position;
			rb.AddForce (wallToP1.normalized * wallKnockbackForce);
		}

		if (other.gameObject.tag == "Player2") { 
			
			if (this.gameObject.transform.position.y > other.gameObject.transform.position.y) { 
				// Decrease player2 score
				ScoreController2.decrementP2Score (levelController.collisionValue);
				levelController.DisplayState ("P1 wins collision");
				Vector3 p1ToP2 = (other.gameObject.transform.position - this.gameObject.transform.position);
				other.gameObject.GetComponent<Rigidbody> ().AddForce (p1ToP2.normalized * knockbackForce);

			} else if (this.gameObject.transform.position.y < other.gameObject.transform.position.y) { 
				ScoreController2.decrementP1Score (levelController.collisionValue);
				levelController.DisplayState ("P2 wins collision");

				Vector3 p2ToP1 = (this.gameObject.transform.position - other.gameObject.transform.position);
				this.gameObject.GetComponent<Rigidbody> ().AddForce (p2ToP1.normalized * knockbackForce);

			}
		}
	}

	public void SpeedUp() { 
		currPowerupTimer = levelController.powerUpTimer;
		powerup = "Speedup";
	}

	public void SlowDown() { 
		currPowerupTimer = levelController.powerUpTimer;
		powerup = "Slowdown";
	}
		
}
