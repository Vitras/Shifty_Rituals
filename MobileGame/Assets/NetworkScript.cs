﻿using UnityEngine;
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
		Debug.Log ("Joined room: " + roomName);
        Debug.Log(PhotonTargets.Others);
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
		Application.LoadLevel (3);
		var script = GameObject.Find ("MasterObject").GetComponent<GameManager>();
        script.goal = goal;
		script.thresh = threshold;
        script.timer = timer;
		this.photonView.RPC ("GameStarted", PhotonTargets.Others);
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
