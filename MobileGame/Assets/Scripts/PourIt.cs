using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PourIt : MonoBehaviour
{
    //goal is waarde van 0-100 hoe vol je moet eindigen, threshold is hoeveel je hieronder mag gaan zonder fail.

    GameManager master;
    GameObject liquid, music;
    private float timer;
    private int goal, threshold, difficulty;
    private float content;
    public Sprite orange, red;
    public Image liquids;


    // Use this for initialization
    void Start()
    {
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        liquid = GameObject.Find("Liquid");
        music = GameObject.Find("Music");
        timer = master.timer;
        goal = master.goal;
        threshold = master.extrain;
        difficulty = master.difficulty;
        content = 100;
        liquids = GameObject.Find("Liquid").GetComponent<Image>();
        if (Random.Range(0, 2) == 1)
            difficulty = 0;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            master.Results(false);
        }
        
        //lees rotation van device
        //stel rotation van content hieropaan
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight || Input.deviceOrientation == DeviceOrientation.FaceUp || Input.deviceOrientation == DeviceOrientation.FaceDown)
        { // Rotated ~90 degrees
            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                liquid.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                liquid.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            //leeglopen moved content sprite down
            float change = 0.1f;
            //liquid.transform.localPosition -= liquid.transform.up * change * 5;
            RectTransform trans = (RectTransform)liquid.GetComponent("RectTransform");
            trans.anchoredPosition3D -= liquid.transform.up * change * 5;
            content -= change;

            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music.GetComponent("AudioSource");
            play.volume = 1.0f;
        }
        else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        { // Rotated ~180 degrees
            liquid.transform.rotation = Quaternion.Euler(0, 0, 180);
            //leeglopen moved content sprite down
            float change = 0.5f;
            //liquid.transform.localPosition -= liquid.transform.up * change * 5;
            RectTransform trans = (RectTransform)liquid.GetComponent("RectTransform");
            trans.anchoredPosition3D -= liquid.transform.up * change * 5;
            content -= change;

            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music.GetComponent("AudioSource");
            play.volume = 1.0f;
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            liquid.transform.rotation = Quaternion.Euler(0, 0, 0);
            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music.GetComponent("AudioSource");
            play.volume = 0.0f;

            //Win op higher difficulty als in een bepaalde range
            if (difficulty >= 5)
            {
                if (content < goal && content > (goal - threshold))
                {
                    master.Results(true);
                }
            }
        }

        //Win op easy difficulty als empty
        if (difficulty <= 4)
        {
            if (content <= 0)
                master.Results(true);
        }
        if (difficulty >= 5)
        {
            //Change water color based on hoe close je bent als warning/ indication
            if (content <= (goal + threshold))
            {
                liquids.sprite = orange;
            }
            if (content <= goal)
            {
                liquids.sprite = red;
            }

            //lose op higher difficulty als in een bepaalde range
            if (difficulty >= 5)
            {
                if (content < (goal - threshold))
                {
                    master.Results(false);
                }
            }
        }
    }
}
