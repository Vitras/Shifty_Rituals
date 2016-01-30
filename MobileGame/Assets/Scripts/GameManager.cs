using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int amountGames = 4;
    public float fuckUp, difficulty;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    // start the main game loop
    public void Startgame()
    {
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel(Random.Range(2,amountGames+1));
        //give the server green light for play
    }

    public void Nextgame()
    {
        // send succes or fail to server
        DontDestroyOnLoad(gameObject);
        int nextLevel = Random.Range(2, amountGames + 1);
        while (nextLevel != Application.loadedLevel)
        {
            nextLevel = Random.Range(2, amountGames+1);
        }
        Application.LoadLevel(nextLevel);

    }
}
