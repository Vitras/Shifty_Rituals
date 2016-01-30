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
		Debug.Log ("Joined room: " + roomName);
	}

	public void OnSubmit(string code)
	{
		PhotonNetwork.JoinRoom (code);
	}

}
