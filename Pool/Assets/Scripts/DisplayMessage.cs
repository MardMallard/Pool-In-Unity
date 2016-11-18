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
	
	public void displayMessage(string message)
	{
		Debug.Log("Displaying message.");
		
		display.gameObject.SetActive(true);
		display.text = message;
		Invoke("disableMessage", 2F);
	}
	
	private void disableMessage()
	{
		display.gameObject.SetActive(false);
	}
}
