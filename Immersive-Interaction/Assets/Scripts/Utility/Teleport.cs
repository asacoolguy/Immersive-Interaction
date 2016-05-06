using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	public GameObject targetLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)){
			transform.position = targetLocation.transform.position;
		}
	}
}
