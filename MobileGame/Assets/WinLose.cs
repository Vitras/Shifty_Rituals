using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class WinLose : MonoBehaviour {

    public AudioClip[] clips;
    public Sprite[] backgrounds;
    private AudioSource source;
    private GameManager master;

	// Use this for initialization
	void Start ()
    {
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        source = GetComponent<AudioSource>();
        Image background = GameObject.Find("Image").GetComponent<Image>();
        if (master.succes)
        {
            source.clip = clips[0];
            background.sprite = backgrounds[0];
        }
        else
        {
            source.clip = clips[1];
            background.sprite = backgrounds[1];
        }
        source.Play();
        StartCoroutine(wait2Sec());

	}
	
	// Update is called once per frame
	IEnumerator wait2Sec()
    {
        yield return new WaitForSeconds(1.5f);
        master.NextGame();
    }
}
