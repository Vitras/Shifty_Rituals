using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TiltIt : MonoBehaviour
{
    //goal is waarde van 0-7 hoeveel crystals er spawnen, threshold is hoever hieraf je moet eindigen.

    GameManager master;
    float timer;
    int goal, threshold, difficulty;
    Image[] crystals;
    int amountOfCrystals;
    public GameObject[] crystalHolders;
    float upsideDownTimer;
    GameObject music;


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
        threshold = 0;//master.threshold;
        difficulty = 0;//master.difficulty;
        for (int x = 0; x < crystals.Length; x++)
        {
            crystals[x] = crystalHolders[x].GetComponent<Image>();
        }
        for (int x = crystals.Length; x < 7; x++)
        {
            crystalHolders[x].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            //master.Results(false);
        }

        
        //als ie op zijn kop ligt
        if (Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            upsideDownTimer += Time.fixedDeltaTime;
             
            //scale de val speed met difficulty zodat game halen mogelijk blijft
            if (difficulty <= 5)
            {
                if (upsideDownTimer >= 1.0f)
                {
                    upsideDownTimer -= 1.0f;
                    crystals[amountOfCrystals].enabled = false;
                    amountOfCrystals--;
                    AudioSource play = (AudioSource)music.GetComponent("AudioSource");
                    play.Play();
                }
            }
            else //(difficulty > 5)
            {
                if (upsideDownTimer >= 0.5f)
                {
                    upsideDownTimer -= 0.5f;
                    crystals[amountOfCrystals].enabled = false;
                    amountOfCrystals--;
                    AudioSource play = (AudioSource)music.GetComponent("AudioSource");
                    play.Play();
                }
            }
        }

        if (amountOfCrystals == threshold)
        {
            //master.Results(true);
        }
    }
}
