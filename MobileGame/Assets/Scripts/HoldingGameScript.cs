﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoldingGameScript : MonoBehaviour {

    //NetworkScript networkManager;
    private GameManager master;
	public ParticleSystem BUBBLES;
	public GameObject b1,b2,b3,b4;
	public GameObject fire;
	public bool firing;
    public float timer;
    public float heat, incRatio, goal;
    GameObject music1, music2;

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
		currentState = BurnerState.Calm;
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        incRatio = master.extraf;

        goal = master.goal;
        timer = master.timer;

        music1 = GameObject.Find("Music1");
        music2 = GameObject.Find("Music2");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            master.Results(false);

		if (heat <= goal / 3)
			currentState = BurnerState.Calm;
		else if (heat <= goal * (0.75)) {
			currentState = BurnerState.Turbulent;
		} else if (heat <= goal) {
            currentState = BurnerState.Done;
            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music2.GetComponent("AudioSource");
            play.volume = 1.0f;
		}
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
		if (Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			fire.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			firing = true;
            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music1.GetComponent("AudioSource");
            play.volume = 1.0f;
		}
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (currentState == BurnerState.Done)
                master.Results(true);
            else if (currentState == BurnerState.Overflowing)
                master.Results(false);
            fire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            firing = false;
            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music1.GetComponent("AudioSource");
            play.volume = 0.0f;
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
