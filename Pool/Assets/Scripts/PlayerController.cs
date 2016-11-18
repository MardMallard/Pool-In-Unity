using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
	private bool dragging = false;
	private Vector3 startMousePosition = Vector3.zero;
	private Vector3 endMousePosition = Vector3.zero;
	private Vector3 offset = Vector3.zero;
	private Behaviour halo = null;
	private bool disableControl = false;
	
	private Player player1;
	private Player player2;
	private Player currentPlayer;
	
	public float magnitude;
	public LineRenderer lineRenderer;
	public Rigidbody[] balls;
	public Text playerLabel;
	public DisplayMessage displayMessage;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
		halo = gameObject.GetComponent("Halo") as Behaviour;
		
		//Initialise players
		player1 = new Player(PlayerNumber.One);
		player2 = new Player(PlayerNumber.Two);
		currentPlayer = null;
		
		//Start the first round
		assessRound();
	}
	
	//Highlight the cue ball on mouseover
	public void OnMouseEnter()
	{
		halo.enabled = true;
	}
	
	public void OnMouseExit()
	{
		//If player is dragging, don't disable the halo.
		if (!Input.GetMouseButton(0))
			halo.enabled = false;
	}
	
	void LateUpdate ()
    {
		//If the player was dragging last frame, check to see if released.
		//If not, do nothing.
		if (dragging)
		{
			//Stopped dragging!
			if (!Input.GetMouseButton(0))
			{
				//Determine the mouse position on the horizontal plane.
				endMousePosition = getMousePosition();
				
				//Determine the offset
				offset = endMousePosition - startMousePosition;
				
				//User is no longer dragging.
				dragging = false;
				halo.enabled = false;
				
				//Apply a force to the ball.
				rigidBody.AddForce(offset * magnitude, ForceMode.Impulse);
				
				//Remove line
				lineRenderer.enabled = false;
			}	
			//Still dragging, draw a line
			else
			{
				lineRenderer.SetPositions(new Vector3[] {startMousePosition, getMousePosition()});
			}
		}
		
		//If any balls are in motion, controls should be disabled.
		bool anyMoving = false;
		foreach (Rigidbody r in balls)
			if (r.gameObject.activeInHierarchy == true && r.velocity.magnitude > 0.001)
				anyMoving = true;
			
		if (rigidBody.velocity.magnitude > 0.001)
			anyMoving = true;
		
		if (anyMoving)
			disableControl = true;
		//If all balls have just come to a stop, start a new round.
		else if (disableControl)
		{
			Debug.Log("All balls have stopped.");
			disableControl = false;
			assessRound();
		}
	}
	
	//For player clicking and dragging the cue ball.
	void OnMouseDown()
	{
		//Player controls are disabled while any of the balls are in motion.
		if (disableControl)
			return;
		
		if (rigidBody.velocity.magnitude > 0.01)
			return;
		
		//Determine the mouse position on the horizonal plane
		startMousePosition = rigidBody.position;
				
		//Add line
		lineRenderer.enabled = true;
				
		//The user is now dragging.
		dragging = true;
	}
	
	//Determine the mouse position on the horizontal plane.
	Vector3 getMousePosition()
	{
		//Determine the mouse position on the horizontal plane.
		Plane plane = new Plane(Vector3.up, rigidBody.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		plane.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}
	
	//Switches control over between player 1 and player 2
	void switchPlayer()
	{
		currentPlayer = otherPlayer(currentPlayer);
	}
	
	void assessRound()
	{
		Debug.Log("Assessing round!");
		
		//It's the first round.
		if (currentPlayer == null)
		{
			Debug.Log("First round.");
			changePlayer(player1);
			return;
		}
		
		//Did the player get a penalty for pocketing the cue ball?
		if (currentPlayer.penalty)
		{
			Debug.Log("Penalty for pocketing the cue ball!");
			currentPlayer.penalty = false;
			Player other = otherPlayer(currentPlayer);
			
			//Give the other player an extra turn.
			other.extraTurn = true;
			//Display stuff
			displayMessage.displayMessage("You pocketed the cue ball!\n Player " 
			+ (int)other.getPlayerNumber() + " gets an extra turn.");
		}
		
		//Does the player get another turn?
		else if (currentPlayer.extraTurn)
		{
			Debug.Log("Player gets another turn!");
			currentPlayer.extraTurn = false;
			//Display stuff for extra turn.
			displayMessage.displayMessage("You get another turn!");
			return;
		}
		
		//Switch the player every round.
		changePlayer(otherPlayer(currentPlayer));
	}
	
	//Everything related to changing the current player.
	void changePlayer(Player player)
	{
		Debug.Log("Switching to player: " + player.getPlayerNumber());
		currentPlayer = player;
		playerLabel.text = "PLAYER " + (int)player.getPlayerNumber();
		displayMessage.displayMessage("Switching to player " 
		+ (int)currentPlayer.getPlayerNumber() + ".");
	}
	
	//Easily switch between players without writing a whole bunch of ifs and elses.
	Player otherPlayer(Player player)
	{
		if (player.getPlayerNumber() == PlayerNumber.One)
			return player2;
		else
			return player1;
	}
	
	public void onBallPocketed(int num)
	{
		//Cue ball pocketed!
		if (num == 0)
		{
			Debug.Log("Penalty.");
			currentPlayer.penalty = true;
		}
		//Eight-ball pocketed!
		else if (num == 8)
		{
			
		}
		//Other ball pocketed
		else
			if (currentPlayer.ballPocketed(num))
				currentPlayer.extraTurn = true;
	}
}
