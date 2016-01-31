using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Masher : MonoBehaviour {

    private GameManager master;
    public Sprite[] MasherSprites;
    public int currentState;
    public Image currentSpriteRenderer;
    public Text[] texts;
    private int mashed = 0, goal, difficulty;
    private float timer;


    // Use this for initialization
    void Start() {
        currentState = 0;
        currentSpriteRenderer = GetComponent<Image>();
        currentSpriteRenderer.sprite = MasherSprites[currentState];
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        goal = master.goal;
        timer = master.timer;
        difficulty = master.difficulty;
        if (Random.Range(0, 2) == 1)
            difficulty = 0;
        if (difficulty <= 4)
        {
            texts[1].enabled = false;
        }
        else
        {
            texts[0].enabled = false;
            texts[1].text = "Mash exactly " + goal.ToString() + " times!";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            master.Results(false);

        int previousState = currentState;
        if (Input.touchCount == 1)
        {
            currentState = 1;
            Debug.Log("Pressing it!");
        }
        else
        {
            currentState = 0;
        }
        currentSpriteRenderer.sprite = MasherSprites[currentState];
        if (previousState == 0 && currentState == 1)
        {
            Debug.Log("Mash!");
            mashed++;
        }

        if (difficulty <= 4)
        {
            if (mashed >= goal)
                master.Results(true);
        }
        else
        {
            if (mashed == goal)
                master.Results(true);
            else if (mashed > goal)
                master.Results(false);
        }
    }
}
