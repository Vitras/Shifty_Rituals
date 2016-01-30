using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour 
{

	public void OnClick()
	{
		string code = GameObject.Find ("RoomCodeInput").GetComponent<Text> ().text;
		GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().OnSubmit(code);

	}

}
