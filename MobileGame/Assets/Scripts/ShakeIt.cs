using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeIt : MonoBehaviour {


    Vector3 lastDirection, newDirection;
    int shakeAmount, beginColour, currImage = 0;
    GameManager master;
    Image flask;
    float framesNoMove;
    bool shaking, to;
    public int goal;
    public float timer, pauzeThreshold;
    public Sprite[] images;
    


	// Use this for initialization
	void Start ()
    {
        lastDirection = Input.acceleration;
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        beginColour = Random.Range(0, 4);
        flask = GameObject.Find("Flask").GetComponent<Image>();
        flask.sprite = images[(beginColour * 3) + currImage];
        timer = master.timer;
        goal = master.goal;
        pauzeThreshold = master.extraf;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            master.Results(false);
        }
        newDirection = Input.acceleration;
        if (newDirection.magnitude <= 0.5)
        {
            framesNoMove += Time.fixedDeltaTime;
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
            to = false;
            flask.sprite = images[(beginColour * 3) + currImage];
        }
        else if (Vector3.Angle(newDirection.normalized, lastDirection.normalized) > 90)
        {
            
            shakeAmount++;
            if (to)
            {
                currImage--;
                to = currImage == 0;
            }
            else
                currImage++;
            flask.sprite = images[(beginColour * 3) + currImage];
			if (shakeAmount >= goal) 
			{
				master.Results(true);
			}
        }
        lastDirection = newDirection;
        
	}
}
