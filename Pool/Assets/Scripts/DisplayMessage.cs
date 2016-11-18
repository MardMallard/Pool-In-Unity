using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayMessage : MonoBehaviour
{
	public Text display;

	// Use this for initialization
	void Start() 
	{}
	
	// Update is called once per frame
	void Update()
	{}
	
	public void displayMessage(string message, float duration)
	{
		display.gameObject.SetActive(true);
		display.text = message;
		
		if (duration != 0)
			Invoke("disableMessage", duration);
	}
	
	private void disableMessage()
	{
		display.gameObject.SetActive(false);
	}
}
