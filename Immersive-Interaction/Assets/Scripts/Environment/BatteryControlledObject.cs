using UnityEngine;
using System.Collections;

public class BatteryControlledObject : MonoBehaviour {
	public bool activated = true;

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
