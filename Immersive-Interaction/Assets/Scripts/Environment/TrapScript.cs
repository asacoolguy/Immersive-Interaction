using UnityEngine;
using System.Collections;

public class TrapScript : BatteryControlledObject {
	public bool batteryActivated = false;
	public bool timeActivated = true;

	public float timeActive = 3f;
	public float timeDeactive = 1f;
	private float currentTime = 0f;

	public bool enabledCurrent = false;
	public bool enabledNext = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
		// turn on the trap accordingly
		if (batteryActivated){
			enabledCurrent = activated;
		}
		else if (timeActivated){
			if (enabledCurrent && timeDeactive > 0 && currentTime > timeActive){
				currentTime = 0f;
				enabledNext = false;
			}
			else if(!enabledCurrent && timeActive > 0 && currentTime > timeDeactive){
				currentTime = 0f;
				enabledNext = true;
			}
			else{
				currentTime += Time.deltaTime;
			}

			if (!enabledCurrent && enabledNext){
				EnableTrap();
				enabledCurrent = enabledNext;
			}
			else if(enabledCurrent && !enabledNext){
				DisableTrap();
				enabledCurrent = enabledNext;
			}
		}
	}

	void OnEnable(){
		enabledNext = true;
	}

	void OnDisable(){
		enabledNext = false;
	}

	public virtual void EnableTrap(){

	}

	public virtual void DisableTrap(){

	}
}
