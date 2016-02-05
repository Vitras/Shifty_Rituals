using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public GameObject slider;
	public GameObject textField;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		textField.GetComponent<Text>().text = slider.GetComponent<Slider> ().value.ToString();
	}
}
