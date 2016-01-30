using UnityEngine;
using System.Collections;

public class Swiping : MonoBehaviour {

    bool swiping;
    Vector2 newPos, oldPos, direction, goodDir;
    float dist, goal, timer;
    int difficulty


    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer -= Time.fixedDeltaTime;
        if (Input.touchCount == 0)
            return;

        Touch swipe = Input.GetTouch(0);
        if (swipe.phase == TouchPhase.Moved)
        {
            newPos = swipe.position;
            swiping = true;
            direction = newPos - oldPos;
            dist = direction.magnitude;
            direction.Normalize();
        }
        else if(swipe.phase == TouchPhase.Ended)
        {
            if (dist>= goal)
        }
        if(swipe.phase == TouchPhase.Began)
        {
            oldPos = swipe.position;
        }
	}
}
