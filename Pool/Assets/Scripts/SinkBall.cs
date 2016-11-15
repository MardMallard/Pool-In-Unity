using UnityEngine;
using System.Collections;

public class SinkBall : MonoBehaviour 
{
	public GameObject[] ballDisplays;
	public GameObject winText;
	
	void OnTriggerEnter(Collider ball)
	{
		string tag = ball.tag;
		
		ball.gameObject.SetActive(false);
		
		switch (tag)
		{
			case "Ball1":
				ballDisplays[0].SetActive(true);
				break;
			case "Ball2":
				ballDisplays[1].SetActive(true);
				break;
			case "Ball3":
				ballDisplays[2].SetActive(true);
				break;
			case "Ball4":
				ballDisplays[3].SetActive(true);
				break;
			case "Ball5":
				ballDisplays[4].SetActive(true);
				break;
			case "Ball6":
				ballDisplays[5].SetActive(true);
				break;
			case "Ball7":
				ballDisplays[6].SetActive(true);
				break;
			case "Ball8":
				ballDisplays[7].SetActive(true);
				break;
			case "Ball9":
				ballDisplays[8].SetActive(true);
				break;
			case "Ball10":
				ballDisplays[9].SetActive(true);
				break;
			case "Ball11":
				ballDisplays[10].SetActive(true);
				break;
			case "Ball12":
				ballDisplays[11].SetActive(true);
				break;
			case "Ball13":
				ballDisplays[12].SetActive(true);
				break;
			case "Ball14":
				ballDisplays[13].SetActive(true);
				break;
			case "Ball15":
				ballDisplays[14].SetActive(true);
				break;
			default:
				ball.gameObject.SetActive(true);
				break;
		}

		checkWinCondition();
	}
	
	void checkWinCondition()
	{
		//Check if they won the game.
		foreach (GameObject g in ballDisplays)
		{
			if (g.activeInHierarchy == false)
				return;
		}
	
		//If this has been reached all the balls have been sunk.
		winText.SetActive(true);
	}
}
