using UnityEngine;
using System.Collections;
using Photon;
using System.Linq;
using UnityEngine.UI;

public class NetworkScript : Photon.PunBehaviour {

	public const int MINIGAME_COUNT = 3;
	public string roomName;
	public string memorisationTerm;
	public int memorisationTurn;

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
			this.photonView.RPC("PlayShakeGame",PhotonTargets.Others, GameObject.Find("Master").GetComponent<Master>().Difficulty); 
			Debug.Log("PlayShakeGame!");

			return;
		case 1: 
			this.photonView.RPC("PlayMashGame",PhotonTargets.Others, GameObject.Find("Master").GetComponent<Master>().Difficulty); 
			Debug.Log("PlayMashGame!");
			return;
		case 2: 
			this.photonView.RPC("PlayHoldGame",PhotonTargets.Others, GameObject.Find("Master").GetComponent<Master>().Difficulty);
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
		if(Random.Range(0, 5) < 3)
		{
		GameObject.Find("Master").GetComponent<Master>().SetGroupTask("Remember the following word for later: ");
		GameObject.Find("MemorisationNotifier").GetComponent<Text>().text = GenerateRoomName(
			Random.Range(3, 3 + GameObject.Find("Master").GetComponent<Master>().Difficulty/2));
			memorisationTurn = Random.Range(2, 6);
		}
		memorisationTurn--;
		if(memorisationTurn == 0)
		{
			this.photonView.RPC("PlayMemorisationGame", PhotonTargets.Others, memorisationTerm);
			Debug.Log("PlayMemorisationGame");
		}
		else
		{
		GameObject.Find("MemorisationNotifier").GetComponent<Text>().text = "";
		GenerateGame(Random.Range(0, MINIGAME_COUNT));
		Debug.Log("NextGame");
		}

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

