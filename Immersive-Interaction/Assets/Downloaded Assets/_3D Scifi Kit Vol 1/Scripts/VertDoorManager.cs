using UnityEngine;
using System.Collections;

public class VertDoorManager : MonoBehaviour {

	public DoorVert door1;
	
	void OnTriggerEnter(Collider c){
		if (door1!=null && c.gameObject.tag == "Hero"){
			door1.OpenDoor();	
		}

	}
}
