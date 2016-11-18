using UnityEngine;
using System.Collections;

public enum PlayerNumber{One = 1, Two = 2};

public class Player
{
	private PlayerNumber playerNumber;
	public bool extraTurn = false;
	public bool pocketedBlack = false;
	public bool penalty = false;
	
	public Player(PlayerNumber playerNumber)
	{
		this.playerNumber = playerNumber;
	}
	
	public PlayerNumber getPlayerNumber()
	{
		return playerNumber;
	}
	
	//Returns true if pocketing this ball awards the player an extra turn.
	public bool isValidBall(int num)
	{
		if (((int)playerNumber == 1 && num > 7) || ((int)playerNumber == 2 && num < 9))
			return false;
		return true;
	}
}
