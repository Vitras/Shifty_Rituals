using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class WinLose : MonoBehaviour {

    public AudioClip[] clips;
    private AudioSource source;
    private GameManager master;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(GameObject.Find("MasterObject"));
        master = GameObject.Find("MasterObject").GetComponent<GameManager>();
        source = GetComponent<AudioSource>();
        if (master.succes)
            source.clip = clips[0];
        else
            source.clip = clips[1];
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
