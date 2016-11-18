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
	
	// Update is called once per frame
	void Update ()
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
			if (r.gameObject.activeInHierarchy == true && r.velocity.magnitude > 0.01)
				anyMoving = true;
			
		if (anyMoving)
			disableControl = true;
		//If all balls have just come to a stop, start a new round.
		else if (disableControl)
		{
			disableControl = false;
			assessRound();
		}
	}
	
	//For player clicking and dragging the cue ball.
	void OnMouseDown()
	{
		//Player controls are disabled while any of the balls are in motion.
		
		
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
			
		}
		//Switch the player every round.
		else
			changePlayer(otherPlayer(currentPlayer));
	}
	
	//Everything related to changing the current player.
	void changePlayer(Player player)
	{
		Debug.Log("Switching to player: " + player.getPlayerNumber());
		currentPlayer = player;
		playerLabel.text = "PLAYER " + (int)player.getPlayerNumber();
	}
	
	//Easily switch between players without writing a whole bunch of ifs and elses.
	Player otherPlayer(Player player)
	{
		if (player.getPlayerNumber() == PlayerNumber.One)
			return player2;
		else
			return player1;
	}
}
