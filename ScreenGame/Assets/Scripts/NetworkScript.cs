using UnityEngine;
using System.Collections;
using Photon;
using System.Linq;
using UnityEngine.UI;

public class NetworkScript : Photon.PunBehaviour {

	public const int MINIGAME_COUNT = 5;
	public string roomName;
	public string memorisationTerm;
	public int memorisationTurn;
    private Master master;

	// Use this for initialization
	public void Start() 
	{
		//connect to photon network
		PhotonNetwork.ConnectUsingSettings("0.0");
        master = GameObject.Find("Master").GetComponent<Master>();
	}

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        master.GameOver();
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
		case 3: 
			this.photonView.RPC("PlayPourGame",PhotonTargets.Others, GameObject.Find("Master").GetComponent<Master>().Difficulty);
			Debug.Log("PlayPourGame!");
			return;
		case 4: 
			this.photonView.RPC("PlayTiltGame",PhotonTargets.Others, GameObject.Find("Master").GetComponent<Master>().Difficulty);
			Debug.Log("PlayTiltGame!");
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
    void IntroNext()
    {
        Application.LoadLevel(Application.loadedLevel+1);
    }

    [PunRPC]
	void SendFirstGame(int playerAmount)
	{
        //do something fancy with these values
        Application.LoadLevel("InGame");
        master.playerCount = playerAmount;
		GenerateGame(Random.Range(0, MINIGAME_COUNT));
		Debug.Log("SendFirstGame");

	}

	[PunRPC]
	void SendNextGame()
	{
		Debug.Log("PickingNextGame");
		if(Random.Range(0, 5) < 3 && memorisationTurn <= 0)
		{
			Debug.Log("memory incoming");
		master.SetGroupTask("Remember the following word for later: ");
		memorisationTerm = GenerateRoomName(
				Random.Range(3, 3 + master.Difficulty/2));

			GameObject.Find("Memorisation").GetComponent<Text>().text = memorisationTerm;
			memorisationTurn = Random.Range(2, 6);
		}
		memorisationTurn--;
		if(memorisationTurn == 0)
		{
			this.photonView.RPC("PlayMemoryGame", PhotonTargets.Others, memorisationTerm);
			Debug.Log("PlayMemoryGame");
		}
		else
		{
		GenerateGame(Random.Range(0, MINIGAME_COUNT));
		Debug.Log("NextGameSent");
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
		master.ApplyResult(success);
		Debug.Log("Game ended");
		GameObject.Find("Memorisation").GetComponent<Text>().text = "";
		master.SetGroupTask("");
		//set some internal variable
		//adjust fuckup meter
	}

		

}

