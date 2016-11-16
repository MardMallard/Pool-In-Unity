using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
	private bool dragging = false;
	private Vector3 startMousePosition = Vector3.zero;
	private Vector3 offset = Vector3.zero;
	public float magnitude;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
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
