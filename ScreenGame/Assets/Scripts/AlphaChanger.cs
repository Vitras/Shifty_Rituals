using UnityEngine;
using System.Collections;

public class AlphaChanger : MonoBehaviour {

	SpriteRenderer renderer;
	public bool rebound;
	public float speed;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		this.renderer.color = new Color(255, 255, 255, 0);
		rebound = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!rebound)
		{
			this.renderer.color = new Color(255, 255, 255, this.renderer.color.a + Time.deltaTime * speed);
			if(this.renderer.color.a >= 1)
			{
				rebound = true;
			}
		}
		else
		{
			this.renderer.color = new Color(255, 255, 255, this.renderer.color.a - Time.deltaTime * speed);
			if(this.renderer.color.a <= 0)
				Application.LoadLevel("Main");
		}
	}
}
