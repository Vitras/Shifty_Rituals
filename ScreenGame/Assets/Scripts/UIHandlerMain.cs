using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerMain : MonoBehaviour {

	Master master;
	// Use this for initialization
	void Start () {
		master = GameObject.Find("Master").GetComponent<Master>();
		SetUI();
	}

	public void SetDifficulty()
	{
		
	}

	public void SetFailure()
	{
	}

	public void SetGroupTask()
	{
	}

	public void SetUI()
	{
		if(master.GameState == 1)
		{
		SetDifficulty();
		SetFailure();
		SetGroupTask();
		}
		else if(master.GameState == 0)
		{
		}
		else 
		{
		}
	}

	void Update () {
		SetUI();
	}
	// Update is called once per frame
}
