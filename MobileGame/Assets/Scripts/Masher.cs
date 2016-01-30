using UnityEngine;
using System.Collections;

public class Masher : MonoBehaviour {

	public GameObject MasherObject;
    private GameManager master;
	public Sprite[] MasherSprites;
	public int currentState;
	public SpriteRenderer currentSpriteRenderer;
    private int mashed = 0, goal;
    private float timer;


	// Use this for initialization
	void Start () {
		currentState = 0;
		currentSpriteRenderer = MasherObject.GetComponent<SpriteRenderer>();
		currentSpriteRenderer.sprite = MasherSprites[currentState];
        master = GameObject.Find(" MasterObject").GetComponent<GameManager>();
        goal = master.goal;
        timer = master.timer;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            master.Results(false);

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
            mashed++;
		}

        if (mashed >= goal)
            master.Results(true);
	}
}
