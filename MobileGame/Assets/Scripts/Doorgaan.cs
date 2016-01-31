using UnityEngine;
using System.Collections;

public class Doorgaan : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount == 0)
            return;
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            DontDestroyOnLoad(GameObject.Find("MasterObject"));
            GameObject.Find("NetworkManager").GetComponent<NetworkScript>().photonView.RPC("IntroNext", PhotonTargets.Others);
            GameObject.Find("NetworkManager").GetComponent<NetworkScript>().photonView.RPC("SendFirstGame", PhotonTargets.Others);
        }

	}
}
