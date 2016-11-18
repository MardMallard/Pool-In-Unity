using UnityEngine;
using System.Collections;

public class PhysicsCorrector : MonoBehaviour 
{
	private Rigidbody rigidBody;
	public float stopThreshold;
	public float slowThreshold;
	public float slowFactor;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update() 
	{
		//Don't mess with anything that is sleeping.
		if (rigidBody.IsSleeping())
			return;
		
		//If the ball is moving under a certain velocity, stop it from moving.
		if (rigidBody.velocity.magnitude < stopThreshold)
			rigidBody.velocity = Vector3.zero;
		//If ball is moving over the velocity, slow it down incrementally based on the speed.
		else if (rigidBody.velocity.magnitude < slowThreshold)
		{
			Vector3 currentVelocity = rigidBody.velocity;
			Vector3 oppositeVelocity = -currentVelocity * slowFactor;
			rigidBody.AddForce(oppositeVelocity);
		}
	}
}
