using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class MemoryScript : MonoBehaviour {

	public Button option1, option2, option3;
	private GameManager master;
	public string answer;

	// Use this for initialization
	void Start () 
	{
		master = GameObject.Find("MasterObject").GetComponent<GameManager>();
		answer = master.memoryWord;
		string bogus1 = GenerateBogusAnswer (answer.Length);
		string bogus2 = GenerateBogusAnswer (answer.Length);

		string[] answers = new string[3];
		int randomSpot = Random.Range (0, 3);

		if (randomSpot == 0) {
			answers [randomSpot] = answer;
			answers [1] = bogus1;
			answers [2] = bogus2;
		} else if (randomSpot == 1) {
			answers [randomSpot] = answer;
			answers [0] = bogus1;
			answers [2] = bogus2;
			
		} 
		else if(randomSpot == 2)
		{
			answers [randomSpot] = answer;
			answers [0] = bogus1;
			answers [1] = bogus2;
		}
			

		option1.GetComponentInChildren<Text> ().text = answers [0];
		option2.GetComponentInChildren<Text> ().text = answers [1];
		option3.GetComponentInChildren<Text> ().text = answers [2];

	}
		

	public string GenerateBogusAnswer(int length)
	{
		const string chars = "BCDFGHJKLMNPQRSTVQXZ";
		return new string (Enumerable.Repeat (chars, length)
			.Select (s => s [Random.Range(0, (s.Length))]).ToArray ());
	}
		

	// Update is called once per frame
	void Update () 
	{
		
	}
}
