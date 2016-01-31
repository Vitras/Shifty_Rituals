using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using UnityEngine.UI;

public class LeaderbordScript : MonoBehaviour {

	Dictionary<string, int> values;
	// Use this for initialization
	void Start () {
		values = new Dictionary<string, int>();
		string line = "";
		Debug.Log(Application.dataPath + "/Resources/Highscores.txt");
			StreamReader reader = new StreamReader(Application.dataPath + "/Resources/Highscores.txt");
		line = reader.ReadLine();
		while(!string.IsNullOrEmpty(line))
		{
			string[] separation = line.Split(' ');
			values.Add(separation[0], int.Parse(separation[1]));
			line = reader.ReadLine();
		}
		reader.Close();
	}
	
	// Update is called once per frame
	void UpdateLeaderboards (string name, int score) {
		values.Add(name, score);
		int lowestvalue = int.MaxValue;
		string lowestvalueName ="";
		foreach(KeyValuePair<string, int> kv in values)
		{
			if(kv.Value < lowestvalue)
			{
				lowestvalue = kv.Value;
				lowestvalueName = kv.Key;
			}
		}
		if(values.ContainsKey(lowestvalueName))
		values.Remove(lowestvalueName);
	}

	void WriteLeaderboards()
	{
		var items = from pair in values
			orderby pair.Value ascending
			select pair;
		StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/Highscores.txt");
		foreach(KeyValuePair<string, int> kv in items)
		{
			writer.WriteLine("{0}: {1}", kv.Key, kv.Value);
		}
	}
}
