using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeIt : MonoBehaviour {


    Vector3 lastDirection, newDirection;
    int shakeAmount, beginColour, framesNoMove, currImage = 0;
    GameObject master;
    Image flask;
    bool shaking;
    public int goal, pauzeThreshold;
    public float timer;
    public Sprite[] images;
    


	// Use this for initialization
	void Start ()
    {
        lastDirection = Input.acceleration;
        master = GameObject.Find("MasterObject");
        beginColour = Random.Range(0, 4);
        flask = GameObject.Find("Flask").GetComponent<Image>();
        flask.sprite = images[(beginColour * 3) + currImage];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            master.GetComponent<GameManager>().Nextgame(false);
        }
        newDirection = Input.acceleration;
        if (newDirection.magnitude <= 0.5)
        {
            framesNoMove++;
            if (framesNoMove > pauzeThreshold)
            {
                flask.sprite = images[(beginColour * 3) + currImage];
                lastDirection = Vector3.zero;
            }
            return;
        }
        if (lastDirection.magnitude == 0)
        {
            currImage++;
            flask.sprite = images[(beginColour * 3) + currImage];
        }
        else if (Vector3.Angle(newDirection.normalized, lastDirection.normalized) > 90)
        {
            shakeAmount++;
            if (currImage == 2)
                currImage--;
            else
                currImage++;
            flask.sprite = images[(beginColour * 3) + currImage];
			if (shakeAmount >= goal) 
			{
				master.GetComponent<GameManager> ().Nextgame (true);
			}
        }
        lastDirection = newDirection;
        
	}
}
