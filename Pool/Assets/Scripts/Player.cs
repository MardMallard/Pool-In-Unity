﻿using UnityEngine;
using System.Collections;

public enum PlayerNumber{One = 1, Two = 2};

public class Player
{
	private PlayerNumber playerNumber;
	public bool extraTurn = false;
	public bool pocketedBlack = false;
	public bool penalty = false;
	private bool[] ballsPocketed = new bool[7]; //true for pocketed, false for not pocketed
	public GameObject[] balls;
	
	public Player(PlayerNumber playerNumber)
	{
		this.playerNumber = playerNumber;
	}
	
	public PlayerNumber getPlayerNumber()
	{
		return playerNumber;
	}
	
	//Updates the ball as being pocketed for this player.
	//If the ball given belonged to this player, returns true.
	//Otherwise, returns false.
	public bool ballPocketed(int num)
	{
		//If the ball wasn't a ball the player was supposed to pocket, return false
		if (((int)playerNumber == 1 && num > 7) || ((int)playerNumber == 2 && num < 9))
			return false;
		
		if ((int)playerNumber == 2)
			num -= 8;
		
		ballsPocketed[num] = true;
		return true;
	}
	
	//Checks whether all the player's balls have been pocketed.
	public bool allBallsArePocketed()
	{
		foreach (bool go in ballsPocketed)
			if (!go)
				return false;
		
		return true;
	}
}
