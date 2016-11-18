using UnityEngine;
using System.Collections;

public class SinkBall : MonoBehaviour 
{
	public GameObject[] ballDisplays;
	public GameObject winText;
	public PlayerController pc;
	
	void OnTriggerEnter(Collider ball)
	{
		//Get a number to represent the ball that was just sunk.
		int ballNum = 0;
		if (ball.tag == null || ball.tag.Length == 0 || !int.TryParse(ball.tag, out ballNum))
		{
			//Parsing the tag into a number was not successful
			//So this is not a ball.
			ball.gameObject.SetActive(false);
			return;
		}
		
		//Cue ball
		if (ballNum == 0)
		{
			//Set the cue ball back on the table
			ball.GetComponent<Rigidbody>().MovePosition(new Vector3(0, 40, 0));
			ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);	
			Debug.Log("Cue ball pocketed.");
		}
		//All other balls
		else
		{
			ballDisplays[ballNum-1].SetActive(true);
			ball.gameObject.SetActive(false);
		}
		
		//Let the player controller know a ball was pocketed.
		pc.onBallPocketed(ballNum);

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
