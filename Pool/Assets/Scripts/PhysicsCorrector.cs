using UnityEngine;
using System.Collections;

public class PhysicsCorrector : MonoBehaviour 
{
	private Rigidbody rigidBody;
	public float velocityThreshold;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update() 
	{
		//If the ball is moving under a certain velocity, put it to sleep.
		if (!rigidBody.IsSleeping() && rigidBody.velocity.magnitude < velocityThreshold)
		{
			rigidBody.velocity = Vector3.zero;
			rigidBody.Sleep();
		}
	}
}
