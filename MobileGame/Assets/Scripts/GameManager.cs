using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int amountGames = 4;
    public float fuckUp, difficulty;
	private NetworkScript networkManager;
    public float timer, thresh;
    public int goal;

	// Use this for initialization
	void Start ()
    {
		networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    // start the main game loop
    public void Startgame()
    {
        DontDestroyOnLoad(gameObject);
        //Application.LoadLevel(Random.Range(2,amountGames+1));
        //give the server green light for play
    }

	public void Nextgame(bool succes)
    {
        // send succes or fail to server
		networkManager.photonView.RPC("GameEnded", PhotonTargets.Others,succes);

        // pull fuckup and difficulty
        DontDestroyOnLoad(gameObject);
        int nextLevel = Random.Range(2, amountGames + 1);
        while (nextLevel != Application.loadedLevel)
        {
            nextLevel = Random.Range(2, amountGames+1);
        }
        Application.LoadLevel(nextLevel);

    }

    public void ReturntoMenu()
    {
        Application.LoadLevel(1);
    }
}
