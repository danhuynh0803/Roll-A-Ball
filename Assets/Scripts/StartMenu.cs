using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public GameObject StartPanel;  
	public GameObject InstructionsPanel;
	public GameObject CreditsPanel;

	// Use this for initialization
	void Start () {
		StartPanel.SetActive (true);		
		InstructionsPanel.SetActive (false);
		CreditsPanel.SetActive (false);
	}
	
	public void StartSingle() {
		LevelController.isSinglePlayer = true;
		Application.LoadLevel (1);
	}

	public void StartTwo() { 
		LevelController.isSinglePlayer = false;
		Application.LoadLevel (2);
	}

	public void Instructions() { 
		StartPanel.SetActive (false);
		CreditsPanel.SetActive (false);
		InstructionsPanel.SetActive (true);

	}

	public void Credits() { 
		StartPanel.SetActive (false);
		InstructionsPanel.SetActive (false);
		CreditsPanel.SetActive (true);
	}

	public void Back() { 
		StartPanel.SetActive (true);		
		InstructionsPanel.SetActive (false);
		CreditsPanel.SetActive (false);
	}
}
