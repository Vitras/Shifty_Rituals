using UnityEngine;
using System.Collections;

public class BubblingIntensifiesScript : MonoBehaviour {

	public Vector3 Initial;
	// Use this for initialization
	void Start () {
		Initial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distortion = Vector3.zero;
//		switch(GameObject.Find("Master").GetComponent<Master>().Difficulty)
//		{
//		case 10: distortion.x = distortion.y = Random.Range(5, 10);
//		case 9: break;
//		case 8:	 distortion.x = distortion.y = Random.Range(3, 5);
//		case 7: break;
//		case 6:  distortion.x = distortion.y = Random.Range(1, 3);
//		case 5: break;
//		case 4: distortion.x = distortion.y = Random.Range(0, 1);
//		case 3: break;
//		case 2: distortion.x = distortion.y = Random.Range(0, 0);
//		case 1: break;
//		default: break;
//		}
		transform.position = Initial + distortion;
	}
}
