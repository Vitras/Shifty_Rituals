using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeIt : MonoBehaviour {


    Vector3 lastDirection, newDirection;
    int shakeAmount, beginColour;
    GameObject master;
    Image flask;
    public int goal;
    public Sprite[] images;


	// Use this for initialization
	void Start ()
    {
        lastDirection = Input.acceleration;
        master = GameObject.Find("MasterObject");
        beginColour = Random.Range(0, 4);
        flask = GameObject.Find("Flask").GetComponent<Image>();
        flask.sprite = images[(beginColour * 3) + 1];
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
