using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

	public float speed;						// Movement speed
	public float jump;						// Jump force
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
				render.material.color = new Color (0.1f, 0.8f, 0.1f, 1.0f);
				speed = 12.0f;
			} else if (powerup.Equals ("Slowdown")) { 
				render.material.color = new Color (0.8f, 0.1f, 0.1f, 1.0f);
				speed = 4.0f;
			}
		} 

		if (currPowerupTimer <= 0.0f) { 
			render.material.color = new Color (0.678f, 0.678f, 0.490f, 1.0f);
			speed = 8.0f;
			powerup = "";
		}

		float moveHorizontal = Input.GetAxis ("Horizontal2");
		float moveVertical = Input.GetAxis ("Vertical2");

		if (canJump && Input.GetKeyDown (KeyCode.RightControl)) {
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
			ScoreController2.decrementP2Score (levelController.wallValue);
			Vector3 wallToP2 = this.gameObject.transform.position - other.gameObject.transform.position;
			rb.AddForce (wallToP2.normalized * wallKnockbackForce);
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
