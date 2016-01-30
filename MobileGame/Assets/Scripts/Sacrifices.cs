using UnityEngine;
using System.Collections;

public class Sacrifices : MonoBehaviour {
    public GameObject holyOne;

	// Use this for initialization
	void Start ()
    {
		//get connected
		//do stuff
		DontDestroyOnLoad(holyOne);
		GameObject.Find("NetworkManager").GetComponent<NetworkScript>().Connect();
		Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
