using UnityEngine;
using System.Collections;

public class ShakeIt : MonoBehaviour {


    Vector3 lastDirection, newDirection;
    int shakeAmount;
    GameObject master;
    public int goal;

	// Use this for initialization
	void Start ()
    {
        lastDirection = Input.acceleration;
        master = GameObject.Find("MasterObject");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        newDirection = Input.acceleration;
        if(Vector3.Angle(newDirection.normalized, lastDirection.normalized) > 90)
        {
            shakeAmount++;
            if (shakeAmount >= goal)
                master.GetComponent<GameManager>().Nextgame();
        }
        
	}
}
