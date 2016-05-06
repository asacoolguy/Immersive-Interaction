using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// use this script to activate/deactivate things when hero walks into it
public class ObjectTrigger : MonoBehaviour {
	public List<GameObject> itemsToTrigger;
	public bool triggered = false;
	public bool activator = true; // activates things

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable(){
		foreach (GameObject obj in itemsToTrigger){
			obj.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider c){
		if (triggered == false && c.gameObject.tag == "Hero"){
			triggered = true;
			foreach (GameObject obj in itemsToTrigger){
				obj.SetActive(activator);
			}
		}
	}
}
