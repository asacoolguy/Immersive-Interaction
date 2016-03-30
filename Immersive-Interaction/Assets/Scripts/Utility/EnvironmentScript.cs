using UnityEngine;
using System.Collections;

public class EnvironmentScript : MonoBehaviour {
	public GameObject currentRoom;
	public GameObject nextRoom;
	public float cameraMoveTime = 1.5f;
	private Vector3 startingPos;
	private Vector3 expectedDistance;
	private Vector3 movedDistance;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		HeroScript.OnRoomEnter += RecenterCamera;
	}

	void OnDisable(){
		HeroScript.OnRoomEnter -= RecenterCamera;
	}
	
	// Update is called once per frame
	void Update () {
		if (nextRoom){
			// if current transform is already at obj, then we're done
			if (Vector3.Distance(expectedDistance, movedDistance) < 0.005f){
				transform.position = startingPos - expectedDistance;
				currentRoom = nextRoom;
				nextRoom = null;
				expectedDistance = Vector3.zero;
				movedDistance = Vector3.zero;
			}
			// move towards nextRoom
			else{
				Vector3 speed = expectedDistance * Time.deltaTime / cameraMoveTime;
				transform.position -= speed;
				movedDistance += speed;
			}
		}

	}

	public void RecenterCamera(GameObject obj){
		if (currentRoom == null){
			currentRoom = obj;
			return;
		}
		// if current room is already obj, then ignore 
		if (currentRoom == obj){
			return;
		}

		if(nextRoom == null || nextRoom != obj){
			nextRoom = obj;
			startingPos = transform.position;
			expectedDistance = nextRoom.transform.position - currentRoom.transform.position;
			movedDistance = Vector3.zero;
		}
	}
}
