using UnityEngine;
using System.Collections;

public class Masher : MonoBehaviour {

	public GameObject MasherObject;
	public Sprite[] MasherSprites;
	public int currentState;
	public SpriteRenderer currentSpriteRenderer;
	// Use this for initialization
	void Start () {
		currentState = 0;
		currentSpriteRenderer = MasherObject.GetComponent<SpriteRenderer>();
		currentSpriteRenderer.sprite = MasherSprites[currentState];
	}
	
	// Update is called once per frame
	void Update () {
		int previousState = currentState;
		if (Input.touchCount == 1)
		{
			Vector3 Touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if(MasherObject.GetComponent<Collider2D>().OverlapPoint(Touch))
			{
				currentState = 1;
				Debug.Log("Pressing it!");
			}
		}
		else
		{
			currentState = 0;
		}
		currentSpriteRenderer.sprite = MasherSprites[currentState];
		if(previousState == 0 && currentState == 1)
		{
			Debug.Log("Mash!");
		}
	}
}
