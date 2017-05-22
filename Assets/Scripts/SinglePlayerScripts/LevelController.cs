using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public GameObject GameOverPanel;							// Display when timer reaches 0
	public GameObject playerSpawnPoint;							// Player1's spawnpoint
	public GameObject p2SpawnPoint;								// Player2's spawnpoint

	public Text timer; 											// Time remaining
	public Text timerShadow;		
	public Text p1Score;										// Player1's score
	public Text p1ScoreShadow;

	public int pointValue = 1;									// Value of each pickup
	public int wallValue = 1;									// Value of each wall collision
	public int fallValue = 10;
	public float powerUpTimer = 5.0f;							// Time powerup lasts for
	public float respawnDelay = 2.0f;							// Time till player respawns

	private PlayerController player;
	private PlayerController player2;

	public static bool isSinglePlayer = true;					// Check if currently a single player scene
	public float gameTimer;
	private float currentTime;
	private bool hadOvertime = false;							// Check if we reached had one overtime	
	private bool hasUpdatedScore = false;
	private bool isGameOver = false;

	void Start () {
		GameOverPanel.SetActive (false);
		player = FindObjectOfType<PlayerController>();
		currentTime = gameTimer;
		hasUpdatedScore = false;
		isGameOver = false;
		ScoreController.RestartScore ();
	}
	
	// Update is called once per frame
	void Update () {

		currentTime -= Time.deltaTime;							// Decrement time
		timer.text = currentTime.ToString("F0");				// Display currentTime using no decimals
		timerShadow.text = currentTime.ToString ("F0");

		if (currentTime <= 0 && !isGameOver) {
			isGameOver = true;
			GameOver ();
		}

		if (isGameOver) {
			DisplayFinalText (hasUpdatedScore);
		}
	}
	
	public void Respawn() 
	{
		StartCoroutine("RespawnCo");
	}
	
	public IEnumerator RespawnCo() 
	{		
		player.GetComponent<Renderer>().enabled = false; 
		yield return new WaitForSeconds(respawnDelay);
		
		player.enabled = true; 
		player.GetComponent<Renderer>().enabled = true; 
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;				// Reset ball to zero velocity
		player.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		// Reset ball to zero angular velocity


		player.transform.position = playerSpawnPoint.transform.position;

	}

	public void waitDelay() 
	{ 
		StartCoroutine(wait ());
	}
	
	IEnumerator wait() 
	{ 
		yield return new WaitForSeconds(respawnDelay);
	}

	
	private void GameOver() 
	{		
		Time.timeScale = 0.0f;
		GameOverPanel.SetActive (true);

		if (isSinglePlayer) {
			// If current score is a high score, update and say "New High Score"
			if (!hasUpdatedScore) {
				if (ScoreController.isHighScore ()) { 
					hasUpdatedScore = true;
					ScoreController.updateHighScore ();
				}
			} 

		}
		
	}
		
	private void DisplayFinalText(bool isHighScore) 
	{
		if (isHighScore) {
			timer.text = "New High Score: " + ScoreController.getP1Score();
			timerShadow.text = "New High Score: " + ScoreController.getP1Score();
			isHighScore = true;
		} else { 
			// Display player's score when timer reaches 0				
			timer.text = "Final Score: " + ScoreController.getP1Score();
			timerShadow.text = "Final Score: " + ScoreController.getP1Score();
		}
	}
}
