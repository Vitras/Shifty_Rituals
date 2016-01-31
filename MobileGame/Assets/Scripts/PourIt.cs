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
    private int content;
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
        Quaternion mobileRotation = Input.gyro.attitude;
        liquid.transform.rotation = new Quaternion(0, 0, mobileRotation.z, mobileRotation.w);

        //als angle > 90 een kant op dan loopt ie leeg met speed based op hoeveel groter
        if (liquid.transform.rotation.z > 90 && liquid.transform.rotation.z < 270)
        {
            //leeglopen moved content sprite down
            int change = (((int)(liquid.transform.rotation.z - 180) ^ 2) / 810);
            liquid.transform.localPosition -= liquid.transform.up * change;
            content -= change;

            //Win op easy difficulty als empty
            if (content <= 0 && difficulty <= 4)
            {
                master.Results(true);
            }
            else if (difficulty >= 5)
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
            }

            //als de angle groter dan 90 zet volume van looped pouring sound aan, als kleiner zet volume uit (en play verder)
            AudioSource play = (AudioSource)music.GetComponent("AudioSource");
            play.volume = 1.0f;
        }
        else
        {
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

    }
}
