using UnityEngine;
using System.Collections;

public class StartScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame()
	{
		Application.LoadLevel("InGame");
	}

	public void ShowOptions()
	{
		
	}

	public void Exit()
	{
		Application.Quit();
	}
}
