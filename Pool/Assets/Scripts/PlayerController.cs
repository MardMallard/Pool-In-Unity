using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
	private bool dragging = false;
	private Vector3 startMousePosition = Vector3.zero;
	private Vector3 offset = Vector3.zero;
	private Behaviour halo = null;
	public float magnitude;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
		halo = gameObject.GetComponent("Halo") as Behaviour;
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
			if (!Input.GetMouseButton(0))
			{
				//Determine the mouse position on the horizontal plane.
				Plane plane = new Plane(Vector3.up, new Vector3(0, -30, 0));
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float distance = 0;
				plane.Raycast(ray, out distance);
				
				//Determine the offset
				offset = ray.GetPoint(distance) - startMousePosition;
				
				//User is no longer dragging.
				dragging = false;
				halo.enabled = false;
				
				//Apply a force to the ball.
				rigidBody.AddForce(offset * magnitude, ForceMode.Impulse);
			}
		}
		//Otherwise, check to see if the player is commencing dragging.
		//If so, set dragging to true and record mouse position
		else
		{
			if (Input.GetMouseButton(0))
			{
				//Determine the mouse position on the horizonal plane
				Plane plane = new Plane(Vector3.up, new Vector3(0, -30, 0));
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float distance = 0;
				plane.Raycast(ray, out distance);
				startMousePosition = ray.GetPoint(distance);
				
				//The user is now dragging.
				dragging = true;
			}
		}
	}
}
