using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController2 : MonoBehaviour {

	public GameObject GameOverPanel;							// Display when timer reaches 0
	public GameObject p1SpawnPoint;								// Player1's spawnpoint
	public GameObject p2SpawnPoint;								// Player2's spawnpoint
	public GameObject Walls;									// Group walls together, removed in overtime

	public Text timer; 											// Time remaining
	public Text timerShadow;		
	public Text announcements; 									// Game state text

	public int pointValue = 3;									// Value of each pickup
	public int wallValue = 1;									// Value of each wall collision
	public int fallValue = 5;									// Deduct for every fall
	public int collisionValue = 3;								// Deduct for losing collision

	public float announcementTimer = 2.0f;						// Time for each announcement
	public float powerUpTimer = 5.0f;							// Time powerup lasts for
	public float respawnDelay = 2.0f;							// Time till player respawns

	private PlayerController1 player1;
	private PlayerController2 player2;

	public static bool isSinglePlayer = true;					// Check if currently a single player scene
	public float gameTimer;
	private float currentTime;
	private float currentStateTime;
	public bool hadOvertime = false;							// Check if we reached had one overtime		
	private bool hasDisplayed = false;

	void Start () {
		GameOverPanel.SetActive (false);
		player1 = FindObjectOfType<PlayerController1>();
		player2 = FindObjectOfType<PlayerController2>();
		currentTime = gameTimer;
		ScoreController2.RestartScore ();
		announcements.text = "";
	}
	
	// Update is called once per frame
	void Update () {

		currentTime -= Time.deltaTime;							// Decrement time
		timer.text = currentTime.ToString("F0");				// Display currentTime using no decimals
		timerShadow.text = currentTime.ToString ("F0");

		currentStateTime -= Time.deltaTime;
		if (currentStateTime <= 0) { 
			announcements.text = "";
		}
			
		if (currentTime <= 15.0f && !hasDisplayed) {
			hasDisplayed = true;
			DisplayState ("15 Seconds Remaining!");
		}

		if (currentTime <= 0) {
			if (!hadOvertime) {
				// Add additional 30 seconds of overtime
				hadOvertime = true;
				currentTime += 30.0f;
				hasDisplayed = false;
				DisplayState ("Overtime!");				
				
				collisionValue = 0;			// Decrease collision value to zero
				fallValue = 50;				// Make overtime round more dependent on collisions
				respawnDelay = 0.0f; 		// Remove respawn delays to keep actions on going
				player1.knockbackForce *= 2;
				Walls.SetActive (false);
			} else { 
				GameOver ();
			}
		}
	}

	public void DisplayState(string text) 
	{
		announcements.text = text;
		currentStateTime = announcementTimer;
	}

	public void Respawn() 
	{
		StartCoroutine("RespawnCo");
	}
	
	public IEnumerator RespawnCo() 
	{		
		player1.GetComponent<Renderer>().enabled = false; 
		yield return new WaitForSeconds(respawnDelay);
		
		player1.enabled = true; 
		player1.GetComponent<Renderer>().enabled = true; 
		player1.GetComponent<Rigidbody> ().velocity = Vector3.zero;				// Reset ball to zero velocity
		player1.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		// Reset ball to zero angular velocity


		player1.transform.position = p1SpawnPoint.transform.position;

	}

	public void Respawn2()
	{
		StartCoroutine ("RespawnCo2");
	}

	public IEnumerator RespawnCo2()
	{
		player2.GetComponent<Renderer>().enabled = false; 
		yield return new WaitForSeconds(respawnDelay);

		player2.enabled = true; 
		player2.GetComponent<Renderer>().enabled = true; 
		player2.GetComponent<Rigidbody> ().velocity = Vector3.zero;				// Reset ball to zero velocity
		player2.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		// Reset ball to zero angular velocity


		player2.transform.position = p2SpawnPoint.transform.position;
	}

	public void waitDelay(float time) 
	{ 
		StartCoroutine(wait (time));
	}
	
	IEnumerator wait(float time) 
	{ 
		yield return new WaitForSeconds(time);
	}

	
	private void GameOver() 
	{		
		Time.timeScale = 0.0f;
		GameOverPanel.SetActive (true);				

		if (ScoreController2.getP1Score () == ScoreController2.getP2Score ()) {
			timer.text = "Draw!";
			timerShadow.text = "Draw!";
		} else {
			string winner = (ScoreController2.getP1Score () > ScoreController2.getP2Score ()) ? "Player 1" : "Player 2";
			timer.text = "Winner: " + winner;
			timerShadow.text = "Winner: " + winner;
		}
	}
		
}
