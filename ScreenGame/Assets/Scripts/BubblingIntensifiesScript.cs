using UnityEngine;
using System.Collections;

public class BubblingIntensifiesScript : MonoBehaviour {

	public Vector3 Initial;
	public int frame;
	// Use this for initialization
	void Start () {
		Initial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		frame++;
		if (frame >= 4)
		{
		Vector3 distortion = Vector3.zero;
		switch(GameObject.Find("Master").GetComponent<Master>().FailureScale)
		{
		case 10: 
		case 9: distortion.x = Random.Range(-10, 10); distortion.y = Random.Range(-10, 10);break;
		case 8:	 
		case 7: distortion.x = Random.Range(-5, 5); distortion.y = Random.Range(-5, 5);break;
		case 6:  
		case 5: distortion.x = Random.Range(-3, 3); distortion.y = Random.Range(-3, 3);break;
		case 4: 
		case 3: distortion.x = Random.Range(-1, 1); distortion.y = Random.Range(-1, 1);break;
		case 2: 
		case 1:
		default: break;
		}
			transform.position = Initial + distortion * 5;
			frame = 0;
		}
	}
}
