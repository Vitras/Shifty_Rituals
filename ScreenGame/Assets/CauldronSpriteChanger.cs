using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CauldronSpriteChanger : MonoBehaviour {

	public Sprite[,] CauldronSheet;
	public Image CauldronSprite;
	public int Current;
	public float FrameTime;
	public float CurrentTime;

	void Start()
	{
		CauldronSheet = new Image[7,5];
		CauldronSprite = this.GetComponent<Image>();
		CauldronSprite.sprite = CauldronSheet[0,0];
		for(int x = 0; x < 7; x++)
			for(int y = 0; y < 5; y++)
		CauldronSheet[0, 0] = Resources.Load<Sprite>("Textures/cauldron" + x.ToString() + "|" + y.ToString());
	}
}
