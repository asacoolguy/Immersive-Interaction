using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameThrowerTrap : TrapScript {
	public List<GameObject> flames;
	public AudioClip flameOpen;
	public AudioClip flameLoop;
	public AudioClip flameClose;
	private AudioSource source;


	// Use this for initialization
	void Awake () {
		source = this.GetComponent<AudioSource>();
		source.clip = flameLoop;
		source.loop = true;
		source.minDistance = 50f;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public override void EnableTrap(){
		foreach (GameObject obj in flames){
			obj.SetActive(true);
		}

		GetComponent<BoxCollider>().enabled = true;
		FlameAudioLoop();
	}

	public override void DisableTrap(){
		foreach (GameObject obj in flames){
			obj.SetActive(false);
		}

		GetComponent<BoxCollider>().enabled = false;
		FlameAudioClose();
	}

	public void FlameAudioLoop()
	{
		source.pitch = Random.Range(0.8f, 1f);
		source.volume = Random.Range(0.8f, 1f);
		source.PlayOneShot(flameOpen);

		source.loop = true;
		source.Play();
	}

	public void FlameAudioClose(){
		source.loop = false;
		source.Stop();

		source.pitch = Random.Range(0.8f, 1f);
		source.volume = Random.Range(0.8f, 1f);
		source.PlayOneShot(flameClose);
	}
}
