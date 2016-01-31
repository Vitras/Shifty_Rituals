using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = GameObject.Find("Master").GetComponent<Master>().AchievedRounds.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
