using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public int currentclip;

	void Start () {
		DontDestroyOnLoad(this);
		currentclip = -1;
		source = this.GetComponent<AudioSource>();
		clips = new AudioClip[]{
			Resources.Load<AudioClip>("SFX/Difficulty"), 
			Resources.Load<AudioClip>("SFX/DifficultyX"),
			Resources.Load<AudioClip>("SFX/Difficulty2"),
			Resources.Load<AudioClip>("SFX/Difficulty2X"),
			Resources.Load<AudioClip>("SFX/Difficulty3"),
			Resources.Load<AudioClip>("SFX/Difficulty3X"),
			Resources.Load<AudioClip>("SFX/Difficulty4"),
			Resources.Load<AudioClip>("SFX/Difficulty4X"),
			Resources.Load<AudioClip>("SFX/Difficulty5"),
			Resources.Load<AudioClip>("SFX/Difficulty5X")};
		ChangeClip();
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
			GameObject.Find("SuccessChecker").GetComponent<Text>().text = "Success!";
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
				GameObject.Find("SuccessChecker").GetComponent<Text>().text = "Failure!";
			}
		}
		SetDifficulty();
	}

	public void GameOver(){
		GameObject.Find("SuccessChecker").GetComponent<Text>().text = "You lose. Good day sir!";
	}


	public void ApplyDifficulty(){
	}

	public void ApplyFailure(){
	}

	public void SetDifficulty(){
	}

	public void SetFailure(){
		FailureScale++;
	}

	public void SetGroupTask(){
	}

	public void ChangeClip(){
		currentclip++;
		if(source.clip != null)
		{
			float ratio = source.time/source.clip.length;
			source.clip = clips[currentclip];
			source.loop = true;
			source.time = ratio * source.clip.length;
			source.Play();
		}
		else
		{
		source.clip = clips[currentclip];
		source.loop = true;
		source.Play();
		}
	}

	// Update is called once per frame
}
