using UnityEngine;
using System.Collections;
using Photon;
using System.Linq;
using UnityEngine.UI;

public class NetworkScript : Photon.PunBehaviour {

	public const int MINIGAME_COUNT = 3;
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
		GameObject.Find("CodeText").GetComponent<Text>().text = roomName[0] + " " + roomName[1] + " " + roomName[2] + " " + roomName[3];
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
	void GenerateGame(int number)
	{
		switch(number)
		{
		case 0: 
			int goal = 10;
			float thresholdSeconds = 1.0f;
			float timer = 12.0f;
			this.photonView.RPC("PlayShakeGame",PhotonTargets.Others,goal,thresholdSeconds,timer); 
			Debug.Log("PlayShakeGame!");
			return;
		case 1: 
			this.photonView.RPC("PlayMashGame",PhotonTargets.Others, 20, 10.0f); 
			Debug.Log("PlayMashGame!");
			return;
		case 2: 
			this.photonView.RPC("PlayHoldGame",PhotonTargets.Others, 15, 3, 10.0f); 
			Debug.Log("PlayHoldGame!");
			return;
		default: return;
		}
	}

	[PunRPC]
	void ChatMessage(string a, string b)
	{
		Debug.Log("ChatMessage " + a + " " + b);
	}

	[PunRPC]
	void SendFirstGame()
	{
		//do something fancy with these values
		GenerateGame(Random.Range(0, MINIGAME_COUNT));
		Application.LoadLevel("InGame");
		Debug.Log("SendFirstGame");

	}

	[PunRPC]
	void SendNextGame()
	{
		//do something fancy with these values
		GenerateGame(Random.Range(0, MINIGAME_COUNT));
		Debug.Log("NextGame");

	}

	[PunRPC]
	void GameStarted()
	{
		Debug.Log("Game started");
		//set some internal variable
	}

	[PunRPC]
	void GameEnded(bool success)
	{
		GameObject.Find("Master").GetComponent<Master>().ApplyResult(success);
		Debug.Log("Game started");
		//set some internal variable
		//adjust fuckup meter
	}

		

}

