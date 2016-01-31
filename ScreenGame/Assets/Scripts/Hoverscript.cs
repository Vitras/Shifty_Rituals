using UnityEngine;
using System.Collections;

public class Hoverscript : MonoBehaviour {

	public float offset = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y + Mathf.Sin (Time.time + offset) / 25, transform.position.y);
	}
}
