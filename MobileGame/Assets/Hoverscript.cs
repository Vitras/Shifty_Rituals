﻿using UnityEngine;
using System.Collections;

public class Hoverscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y + Mathf.Sin (Time.time) / 250.0f, transform.position.y);
	}
}
