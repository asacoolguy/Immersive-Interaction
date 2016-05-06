using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovableObject : MonoBehaviour {
	public List<GameObject> movableObjects;

	// Use this for initialization
	void Start () {
		movableObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Battery" || c.gameObject.tag == "Movable"){
			if (!movableObjects.Contains(c.gameObject)){
				movableObjects.Add(c.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Battery" || c.gameObject.tag == "Movable"){
			if (movableObjects.Contains(c.gameObject)){
				movableObjects.Remove(c.gameObject);
			}
		}
	}
}
