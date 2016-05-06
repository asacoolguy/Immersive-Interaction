using UnityEngine;
using System.Collections;

public class RespawnAreaScript : MonoBehaviour {
	public GameObject roomTrigger; // corresponding roomTrigger

	// Use this for initialization
	void Start () {
		roomTrigger = transform.parent.Find("Trigger").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
