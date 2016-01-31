using UnityEngine;
using System.Collections;
using Photon;
using System.Linq;

public class NetworkScript : Photon.PunBehaviour {


	protected string roomName;

	// Use this for initialization
	public void Connect() 
	{
		//connect to photon network
		PhotonNetwork.ConnectUsingSettings("0.0");
		

	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
		
	public override void OnJoinedLobby ()
	{
		Debug.Log ("Joined the lobby successfully");
	}

	public override void OnJoinedRoom ()
	{
        //send player amount
		this.photonView.RPC ("SendFirstGame", PhotonTargets.Others);
	}

	public override void OnPhotonJoinRoomFailed (object[] codeAndMsg)
	{
		Debug.Log ("Joining room failed");
	}


	public void OnSubmit(string code)
	{
		PhotonNetwork.JoinRoom (code);
		roomName = code;
	}

	[PunRPC]
	void ChatMessage(string a, string b)
	{
		Debug.Log("ChatMessage " + a + " " + b);
	}

	[PunRPC]
	void PlayShakeGame(int difficulty)
	{
        Debug.Log("need to start game");
		Application.LoadLevel (2);
        Debug.Log("gameloading");
		var script = GameObject.Find ("MasterObject").GetComponent<GameManager>();
        script.difficulty = difficulty;
        script.goal = Mathf.Clamp(10+2*difficulty+Random.Range(-5,6),10,70);
		script.extraf = 30-2*difficulty;
        script.timer = 15+Random.Range(-1f,1f)*difficulty;
		this.photonView.RPC ("GameStarted", PhotonTargets.Others);
	}

    [PunRPC]
    void PlayMashGame(int difficulty)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(3);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = 10*difficulty+10+Random.Range(-5,6);
        script.timer = 15 + Random.Range(-1f, 1f) * difficulty;
        script.difficulty = difficulty;
        this.photonView.RPC("GameStarted", PhotonTargets.Others);
    }

    [PunRPC]
    void PlayHoldGame(int difficulty)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(4);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = 100+5*difficulty+Random.Range(-10,11);
        script.extrain = script.goal/7 - Random.Range(5,11);
        script.timer = 15 + Random.Range(-1f, 1f) * difficulty;
        script.difficulty = difficulty;
        this.photonView.RPC("GameStarted", PhotonTargets.Others);
    }

    void PlayPourGame(int difficulty)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(5);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = Random.Range(0,100);
        script.extrain = 100/difficulty + Random.Range(-5,6);
        script.timer = 15 + Random.Range(-1f, 1f) * difficulty;
        script.difficulty = difficulty;
        this.photonView.RPC("GameStarted", PhotonTargets.Others);
    }

    void PlayTiltGame(int difficulty)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(6);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = Mathf.Clamp(Random.Range(0,8) + difficulty,1,7);
        script.extrain = Random.Range(1,script.goal+1);
        script.timer = 15 + Random.Range(-1f, 1f) * difficulty;
        script.difficulty = difficulty;
        this.photonView.RPC("GameStarted", PhotonTargets.Others);
    }

	void PlayMemoryGame(string answer)
	{
		Debug.Log("need to start game");
		Application.LoadLevel(7);
		Debug.Log("gameloading");
		var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
		script.memoryWord = answer;
		this.photonView.RPC("GameStarted", PhotonTargets.Others);
	}


    [PunRPC]
	void GetValues(float difficulty, float fuckUp)
	{
		
	}

	[PunRPC]
	void GameOver()
	{
		Application.LoadLevel (0);
	}
		

}
