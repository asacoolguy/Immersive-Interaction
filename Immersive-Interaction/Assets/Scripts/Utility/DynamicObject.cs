using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicObject : MonoBehaviour {
	public bool activated = false;
	public List<GameObject> dynamicObjects;

	// Use this for initialization
	void Start () {
		foreach (GameObject obj in dynamicObjects){
			if (obj != null){
				obj.gameObject.SetActive(false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// if we should currently be fading in
		if (activated){
			foreach (GameObject obj in dynamicObjects){
				if (obj != null){
					obj.gameObject.SetActive(true);
					if (obj.GetComponent<AudioSource>() != null && obj.GetComponent<AudioSource>().isPlaying == false){
						obj.GetComponent<AudioSource>().Play();
					}
				}
			}
		}
		// otherwise we're fading out
		else{
			foreach (GameObject obj in dynamicObjects){
				if (obj != null){
					obj.gameObject.SetActive(false);
					if (obj.GetComponent<AudioSource>() != null){
						obj.GetComponent<AudioSource>().Stop();
					}
				}
			}
		}
	}

	public void ActivateObjects(){
		activated = true;
	}

	public void DeactivateObjects(){
		activated = false;
	}
}
