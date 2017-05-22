﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController2 : MonoBehaviour {

	private static int p1Score;					// Player 1's score
	private static int p2Score;					// Player 2's score	
	private static int highScore; 				// Current high score, resets if program is turned off

	public Text p1ScoreText;
	public Text p1ScoreShadowText;
	public Text p2ScoreText; 
	public Text p2ScoreShadowText;

	void Start () {
		p1Score = 0;
		p2Score = 0;

	}

	void Update() { 
		p1ScoreText.text = "P1 Score: " + ScoreController2.p1Score;				// Display player1's score
		p1ScoreShadowText.text = "P1 Score: " + ScoreController2.p1Score;
		p2ScoreText.text = "P2 Score: " + ScoreController2.p2Score;				// Display player2's score
		p2ScoreShadowText.text = "P2 Score: " + ScoreController2.p2Score;
	}

	public static void RestartScore() { 
		p1Score = 0;
		p2Score = 0;
	}

	public static void incrementP1Score(int pointValue) 
	{
		p1Score += pointValue;
	}

	public static void incrementP2Score(int pointValue) 
	{
		p2Score += pointValue;		
	}

	public static void decrementP1Score(int pointValue) 
	{
		p1Score -= pointValue;
	}

	public static void decrementP2Score(int pointValue) 
	{
		p2Score -= pointValue;		
	}
		
	public static int getP1Score() 
	{
		return ScoreController2.p1Score;
	}

	public static int getP2Score() 
	{
		return ScoreController2.p2Score;
	}

	public static bool isHighScore() 
	{
		if (ScoreController2.p1Score > ScoreController2.highScore) {
			return true;
		} 
		return false; 
	}

	public static void updateHighScore() 
	{
		ScoreController2.highScore = ScoreController2.p1Score;	
	}
}

