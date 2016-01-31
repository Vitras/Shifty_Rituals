using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TiltIt : MonoBehaviour
{
    //goal is waarde van 1-7 hoeveel crystals er spawnen, threshold is hoever hieraf je moet eindigen.

    GameManager master;
    float timer;
    int goal, threshold, difficulty;
    Image[] crystals;
    int amountOfCrystals;
    public GameObject[] crystalHolders;
    float upsideDownTimer;
    GameObject music;
    public Text[] texts;


    // Use this for initialization
    void Start()
    {
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        music = GameObject.Find("Music");
        timer = master.timer;
        goal = master.goal;
        amountOfCrystals = goal;
        upsideDownTimer = 0;
        crystals = new Image[goal];
        threshold = master.extrain;
        difficulty = master.difficulty;
        for (int x = 0; x < crystals.Length; x++)
        {
            crystals[x] = crystalHolders[x].GetComponent<Image>();
        }
        for (int x = crystals.Length; x < 7; x++)
        {
            crystalHolders[x].GetComponent<Image>().enabled = false;
        }
        Input.gyro.enabled = true;
        if (Random.Range(0, 2) == 1)
            difficulty = 0;
        if (difficulty<=5)
        {
            texts[1].enabled = false;
        }
        else
        {
            texts[0].enabled = true;
            texts[1].text = "Drop exactly " + threshold.ToString() + " crystals!";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            master.Results(false);
        }

        
        //als ie op zijn kop ligt
        if (Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            //scale de val speed met difficulty zodat game halen mogelijk blijft
            if (difficulty <= 5)
            {
                upsideDownTimer += Time.fixedDeltaTime;
                if (upsideDownTimer >= 1.0f)
                {
                    upsideDownTimer -= 1.0f;
                    crystals[amountOfCrystals - 1].enabled = false;
                    amountOfCrystals--;
                    AudioSource play = (AudioSource)music.GetComponent("AudioSource");
                    play.Play();
                }
            }
            else //(difficulty > 5)
            {
                upsideDownTimer += Time.fixedDeltaTime;
                if (upsideDownTimer >= 0.5f)
                {
                    upsideDownTimer -= 0.5f;
                    crystals[amountOfCrystals - 1].enabled = false;
                    amountOfCrystals--;
                    AudioSource play = (AudioSource)music.GetComponent("AudioSource");
                    play.Play();

                    if (amountOfCrystals < (goal - threshold))
                        master.Results(false);
                }
            }
        }
        else
        {
            if (amountOfCrystals == goal - threshold)
            {
                master.Results(true);
            }
        }
    }
}
