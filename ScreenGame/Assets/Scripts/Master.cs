using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour {

	public int FailureScale;
	public int Difficulty;
	public string GroupTask;
	public double ElapsedTime;
	public int AchievedRounds;
	// Use this for initialization
	void Start () {
	}

	public void Reset(){
		FailureScale = 1;
		Difficulty = 1;
		GroupTask = "";
		AchievedRounds = 0;
		SetUI();
	}

	public void ApplyResult(bool success){
		if(success)
		{
			AchievedRounds++;
		}
		else
		{
		}
	}

	public void GameOver(){
	}

	public void ApplyDifficulty(){
	}

	public void ApplyFailure(){
	}

	public void SetDifficulty(){
	}

	public void SetGroupTask(){
	}

	void Update(){
		if(SceneManagerHelper.ActiveSceneName == "TheRealMain")
		{
			
		}
	}

	// Update is called once per frame
}
