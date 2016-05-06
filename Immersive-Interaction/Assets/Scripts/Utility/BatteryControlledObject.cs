using UnityEngine;
using System.Collections;

public class BatteryControlledObject : MonoBehaviour {
	public bool activated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setActivated(bool b){
		activated = b;
	}
}
