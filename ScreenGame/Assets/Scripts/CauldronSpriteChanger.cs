using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CauldronSpriteChanger : MonoBehaviour {

	public Sprite[,] CauldronSheet;
	public Image CauldronSprite;
	public float FrameTime;
	public float CurrentTime;
	public bool Active;
	public int CurrentFrame;
	public int Frames;

	void Start()
	{
		CauldronSheet = new Sprite[7,5];
		CauldronSprite = this.GetComponent<Image>();
		for(int x = 0; x < 7; x++)
			for(int y = 0; y < 5; y++)
				CauldronSheet[x, y] = Resources.Load<Sprite>("Textures/cauldron" + (x + 1).ToString() + "X" + (y + 1).ToString());
		CauldronSprite.sprite = CauldronSheet[0,0];
		Frames = 7;
		CurrentFrame = 0;
	}

	void Update()
	{
		if(Active)
		{
			CurrentTime += Time.deltaTime;
			if (CurrentTime >= FrameTime)
			{
				CurrentTime = 0;
				CurrentFrame = (CurrentFrame + 1) % Frames;
				CauldronSprite.sprite = CauldronSheet[CurrentFrame, Mathf.Min((GameObject.Find("Master").GetComponent<Master>().Difficulty - 1), 10) / 2];
			}
		}
	}
}
