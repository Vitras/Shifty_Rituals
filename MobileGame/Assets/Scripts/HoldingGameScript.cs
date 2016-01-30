using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoldingGameScript : MonoBehaviour {

    //NetworkScript networkManager;
    private GameManager master;
	public ParticleSystem BUBBLES;
	public GameObject b1,b2,b3,b4;
	public GameObject fire;
	public bool firing;
    private float timer;
    private int heat, targetHeat, incRatio, stateReach;

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
		//networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ();
		currentState = BurnerState.Calm;
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            master.Results(false);
        if (heat <= stateReach)
            currentState = BurnerState.Calm;
        else if (heat <= 2 * stateReach)
            currentState = BurnerState.Turbulent;
        else if (heat <= 3 * stateReach)
            currentState = BurnerState.Done;
        else
            currentState = BurnerState.Overflowing;

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

        if (firing)
        {
            heat += incRatio;
        }

        if (Input.touchCount == 0)
            return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            fire.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (currentState == BurnerState.Done)
                master.Results(true);
            else if (currentState == BurnerState.Overflowing)
                master.Results(false);
            fire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
         


    }

	public void ActivateFire()
	{
        if (Input.touchCount == 0)
            return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            fire.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
            fire.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
	}


}
