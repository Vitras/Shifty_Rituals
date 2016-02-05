using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeIt : MonoBehaviour {


    Vector3 lastDirection, newDirection;
    int shakeAmount, beginColour, currImage = 1;
    GameManager master;
    Image flask;
    float framesNoMove;
    bool shaking, to;
    public int goal;
    public float timer, pauzeThreshold;
    public Sprite[] images;
    GameObject music;
    


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
        music = GameObject.Find("Music");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            master.Results(false);
        }
        newDirection = Input.acceleration;
        if (newDirection.magnitude <= 3)
        {
            framesNoMove += Time.fixedDeltaTime;
            if (framesNoMove > pauzeThreshold)
            {
                currImage = 1;
                flask.sprite = images[(beginColour * 3) + currImage];
                lastDirection = Vector3.zero;
                //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
                AudioSource play = (AudioSource)music.GetComponent("AudioSource");
                play.volume = 0.0f;
            }
            return;
        }
        if (lastDirection.magnitude == 0)
        {
            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music.GetComponent("AudioSource");
            play.volume = 1.0f;
            currImage++;
            to = false;
            flask.sprite = images[(beginColour * 3) + currImage];
        }
        else
        {
            if (Vector3.Angle(newDirection.normalized, lastDirection.normalized) > 90)
            {

                shakeAmount++;
                if (shakeAmount >= goal)
                {
                    master.Results(true);
                }
            }
            if (to)
            {
                currImage++;
                to = currImage != 2;
            }
            else
            {
                currImage++;
                to = currImage == 0;
            }
            Debug.Log(currImage);
                flask.sprite = images[(beginColour * 3) + currImage];
        }
        lastDirection = newDirection;
    }
}
