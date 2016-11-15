using UnityEngine;
using System.Collections;

public class SinkBall : MonoBehaviour 
{
	void OnTriggerEnter(Collider ball)
	{
		ball.gameObject.SetActive(false);
		Debug.Log("Deactivated ball");
	}
}
