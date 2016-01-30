using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour {

	public int FailureScale;
	public int Difficulty;
	public string GroupTask;
	public double ElapsedTime;
	public int AchievedRounds;
	public int GameState; //0 = stationary, 1 = playing, 2 = gameover
	// Use this for initialization
	public AudioSource source;
	public AudioClip[] clips;

	void Start () {
		DontDestroyOnLoad(this);
		source = this.GetComponent<AudioSource>();
		clips = new AudioClip[]{Resources.Load<AudioClip>("SFX/Difficulty")};
		ChangeClip(0);
	}

	public void Reset(){
		FailureScale = 1;
		Difficulty = 1;
		GroupTask = "";
		AchievedRounds = 0;
	}

	public void ApplyResult(bool success){
		if(success)
		{
			AchievedRounds++;
		}
		else
		{
			SetFailure();
			if(FailureScale > 10)
			{
				GameOver();
			}
			else
			{
				AchievedRounds++;
			}
		}
		SetDifficulty();
	}

	public void GameOver(){
	}

	public void ApplyDifficulty(){
	}

	public void ApplyFailure(){
	}

	public void SetDifficulty(){
	}

	public void SetFailure(){
	}

	public void SetGroupTask(){
	}

	public void ChangeClip(int number){
		source.clip = clips[number];
		source.loop = true;
		source.Play();
	}

	// Update is called once per frame
}
