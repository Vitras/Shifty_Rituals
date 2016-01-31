using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int amountGames = 4;
	private NetworkScript networkManager;
    public float timer, extraf;
    public int goal, extrain, difficulty;
    public bool succes;
	public string memoryWord;

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

	public void Results(bool succes)
    {
        this.succes = succes;
        // send succes or fail to server
		networkManager.photonView.RPC("GameEnded", PhotonTargets.Others,succes);

        // pull fuckup and difficulty
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel(6);

    }

    public void NextGame()
    {
        networkManager.photonView.RPC("SendFirstGame", PhotonTargets.Others);
    }
    public void ReturntoMenu()
    {
        Application.LoadLevel(1);
    }
}
