using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoldingGameScript : MonoBehaviour {

	//NetworkScript networkManager;
	public ParticleSystem BUBBLES;
	public GameObject b1,b2,b3,b4;
	public GameObject fire;
	public bool fireToggle;


	public enum BurnerState
	{
		Calm,
		Turbulent,
		Done,
		Overflowing
	}

	public BurnerState currentState;

	// Use this for initialization
	void Start () 
	{
		fireToggle = false;
		//networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ();
		currentState = BurnerState.Calm;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		switch (currentState) 
		{
		case BurnerState.Calm:
			b1.SetActive (true);
			b2.SetActive (false);
			b3.SetActive (false);
			b4.SetActive (false);
			BUBBLES.startColor = Color.white;
			break;
		case BurnerState.Turbulent:
			b1.SetActive (false);
			b2.SetActive (true);
			b3.SetActive (false);
			b4.SetActive (false);
			BUBBLES.startSpeed = 0.3f;
			BUBBLES.startColor = Color.white;
			break;
		case BurnerState.Done:
			b1.SetActive (false);
			b2.SetActive (false);
			b3.SetActive (true);
			b4.SetActive (false);
			BUBBLES.startColor = Color.cyan;
			break;
		case BurnerState.Overflowing:
			b1.SetActive (false);
			b2.SetActive (false);
			b3.SetActive (false);
			b4.SetActive (true);
			BUBBLES.startSpeed = 0.0001f;
			BUBBLES.gravityModifier = 0.81f;
			BUBBLES.startColor = Color.black;
			break;
		}



	}

	public void ActivateFire()
	{
		if (!fireToggle) {
			fireToggle = true;
			fire.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		} 
		else 
		{
			fireToggle = false;
			fire.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}
	}


}
