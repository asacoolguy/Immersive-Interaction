using UnityEngine;
using System.Collections;

public class GameWinScript : MonoBehaviour {
	public GameObject winText;
	public AudioClip winSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		print(c.gameObject.name);
		if (c.gameObject.tag == "Hero"){
			GetComponent<AudioSource>().PlayOneShot(winSound);
			winText.SetActive(true);
		}
	}
}
