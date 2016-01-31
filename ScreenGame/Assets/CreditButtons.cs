using UnityEngine;
using System.Collections;

public class CreditButtons : MonoBehaviour {

	public GameObject creditPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenCredits()
	{
		creditPanel.SetActive(true);
	}

	public void CloseCredits()
	{
		creditPanel.SetActive(false);
	}
}
