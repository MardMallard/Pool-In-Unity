using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
	private bool dragging = false;
	private Vector3 startMousePosition = Vector3.zero;
	private Vector3 offset = Vector3.zero;
	public int magnitude = 1;

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
			if (!Input.GetMouseButtonDown(0))
			{
				offset = Input.mousePosition - startMousePosition;
				dragging = false;
				
				rigidBody.AddForce(offset * magnitude, ForceMode.Impulse);
				Debug.Log(offset * magnitude);
			}
		}
		//Otherwise, check to see if the player is commencing dragging.
		//If so, set dragging to true and record mouse position
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				startMousePosition = Input.mousePosition;
				dragging = true;
			}
		}
	}
}
