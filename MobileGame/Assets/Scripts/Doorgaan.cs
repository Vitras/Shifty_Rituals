using UnityEngine;
using System.Collections;

public class Doorgaan : MonoBehaviour {

    private NetworkScript network;

	// Use this for initialization
	void Start ()
    {
        network = GameObject.Find("NetworkManager").GetComponent<NetworkScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (Application.loadedLevel == 2)
            {
                DontDestroyOnLoad(GameObject.Find("MasterObject"));
                network.photonView.RPC("SendFirstGame", PhotonTargets.Others, network.players);
            }
            else
            {
                DontDestroyOnLoad(GameObject.Find("MasterObject"));
                PhotonNetwork.LeaveRoom();
                Application.LoadLevel("menu");
            }
        }

	}
}
