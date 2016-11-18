using UnityEngine;
using System.Collections;

public enum PlayerNumber{One = 1, Two = 2};

public class Player
{
	private PlayerNumber playerNumber;
	private bool extraTurn = false;
	private bool pocketedBlack = false;
	private bool penalty = false;
	public GameObject[] balls;
	
	public Player(PlayerNumber playerNumber)
	{
		this.playerNumber = playerNumber;
	}
	
	public PlayerNumber getPlayerNumber()
	{
		return playerNumber;
	}
}
