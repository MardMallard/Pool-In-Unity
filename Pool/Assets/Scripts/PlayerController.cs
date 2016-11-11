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
				offset = startMousePosition - Input.mousePosition;
				//We want the ball to move on the x and z axes, not x and y.
				offset.Set(offset.x, 0, offset.y); 
				dragging = false;
				
				rigidBody.AddForce(offset * magnitude, ForceMode.Impulse);
			}
		}
		//Otherwise, check to see if the player is commencing dragging.
		//If so, set dragging to true and record mouse position
		else
		{
			if (Input.GetMouseButton(0))
			{
				startMousePosition = Input.mousePosition;
				dragging = true;
			}
		}
	}
}
