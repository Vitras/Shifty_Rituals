using UnityEngine;
using System.Collections;
using Photon;
using System.Linq;

public class NetworkScript : Photon.PunBehaviour {


	public string roomName;

	// Use this for initialization
	public void Start() 
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
		roomName = GenerateRoomName (4);
		RoomOptions options = new RoomOptions (){isVisible = true,maxPlayers = 2};
		PhotonNetwork.CreateRoom (roomName, options, TypedLobby.Default);
	}

	public override void OnJoinedRoom ()
	{
		Debug.Log ("Joined room: " + roomName);
	}

	public string GenerateRoomName(int length)
	{
		const string chars = "BCDFGHJKLMNPQRSTVQXZ";
		return new string (Enumerable.Repeat (chars, length)
			.Select (s => s [Random.Range(0, (s.Length))]).ToArray ());
	}

	[PunRPC]
	void ChatMessage(string a, string b)
	{
		Debug.Log("ChatMessage " + a + " " + b);
	}
		

}

