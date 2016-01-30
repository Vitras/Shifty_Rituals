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
	void PlayShakeGame(int goal, float threshold, float timer)
	{
        Debug.Log("need to start game");
		Application.LoadLevel (2);
        Debug.Log("gameloading");
		var script = GameObject.Find ("MasterObject").GetComponent<GameManager>();
        script.goal = goal;
		script.extraf = threshold;
        script.timer = timer;
		this.photonView.RPC ("GameStarted", PhotonTargets.Others);
	}

    [PunRPC]
    void PlayMashGame(int goal, float threshold, float timer)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(3);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = goal;
        script.timer = timer;
        this.photonView.RPC("GameStarted", PhotonTargets.Others);
    }

    [PunRPC]
    void PlayHoldGame(int goal, int incRatio, float timer)
    {
        Debug.Log("need to start game");
        Application.LoadLevel(4);
        Debug.Log("gameloading");
        var script = GameObject.Find("MasterObject").GetComponent<GameManager>();
        script.goal = goal;
        script.extrain = incRatio;
        script.timer = timer;
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
