using UnityEngine;
using System.Collections;

public class EnvironmentScript : MonoBehaviour {
	// variables for moving camera
	public GameObject currentRoom;
	public GameObject nextRoom;

	public float cameraMoveTime = 1.5f;
	public float currentMoveTime = 0;
	private Vector3 startingPos;
	private Vector3 expectedDistance;
	private Vector3 movedDistance;

	// variables for respawn
	public float respawnWaitTime = 3f;
	public GameObject hero;

	public AudioClip deathSound;
	public AudioClip respawnSound;

	// Use this for initialization
	void Start () {
		if (currentRoom){
			RoomWallFadeOut(currentRoom);
		}
	}
		
	void OnEnable(){
		EventManagerScript.OnRoomEnter += FocusOnRoom;
		EventManagerScript.OnPlayerDeath += RespawnPlayer;
	}

	void OnDisable(){
		EventManagerScript.OnRoomEnter -= FocusOnRoom;
		EventManagerScript.OnPlayerDeath -= RespawnPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		if (nextRoom){
			// if current transform is already at obj, then we're done
			if (Vector3.Distance(expectedDistance, movedDistance) < 0.01f || currentMoveTime > cameraMoveTime){
			//if (Vector3.Distance(expectedDistance, movedDistance) < 0.4f){
				//RemoveDynamicObjects(currentRoom);
				currentMoveTime = 0;
				transform.position = startingPos - expectedDistance;
				currentRoom = nextRoom;
				nextRoom = null;
				expectedDistance = Vector3.zero;
				movedDistance = Vector3.zero;

				// turn player-hand collision back on
				Physics.IgnoreLayerCollision(9, 11, false);
			}
			// move towards nextRoom
			else{
				Vector3 speed;
				if (cameraMoveTime > 0){
					speed = expectedDistance * Time.deltaTime / cameraMoveTime;
				}
				else{
					speed = expectedDistance;
				}

				transform.position -= speed;
				movedDistance += speed;
				currentMoveTime += Time.deltaTime;
			}
		}

	}

	// handles all the things involved with moving from room to room
	public void FocusOnRoom(GameObject roomTrigger){
		if (currentRoom == null){
			currentRoom = roomTrigger;
			return;
		}
		// if current room is already obj, then ignore 
		if (currentRoom == roomTrigger){
			return;
		}

		if(nextRoom == null || nextRoom != roomTrigger){
			// recenters the camera
			nextRoom = roomTrigger;
			startingPos = transform.position;
			expectedDistance = nextRoom.transform.position - currentRoom.transform.position;
			expectedDistance = new Vector3(expectedDistance.x, expectedDistance.y, 0);
			movedDistance = Vector3.zero;

			// fades in and out the walls of the rooms accordingly
			RoomWallFadeOut(nextRoom);
			RoomWallFadeIn(currentRoom);

			// enables/diables dynamic objects in the rooms
			ActivateDynamicObjects(nextRoom);
			DeactivateDynamicObjects(currentRoom);

			// turns off hand-player collision
			Physics.IgnoreLayerCollision(9, 11, true);

			UpdateMovableObjects(nextRoom);
		}
	}

	public void RespawnPlayer(GameObject respawnArea){
		// move the camera to the respawn area's paired up trigger
		FocusOnRoom(respawnArea.GetComponent<RespawnAreaScript>().roomTrigger);
		GetComponent<AudioSource>().PlayOneShot(deathSound);

		StartCoroutine(RespawnPlayerAt(respawnArea));
	}

	IEnumerator RespawnPlayerAt(GameObject respawnArea){
		yield return new WaitForSeconds(respawnWaitTime);

		// make sure there are no players already
		GameObject[] players = GameObject.FindGameObjectsWithTag("Hero");
		if (players.Length == 0){
			GameObject newHero = Instantiate(hero, respawnArea.transform.position, Quaternion.identity) as GameObject;
			newHero.transform.parent = this.transform;
			newHero.transform.localScale = new Vector3(1,1,1);
			GetComponent<AudioSource>().PlayOneShot(respawnSound);
		}
	}

	// helper function to fade a room's front walls out. Use on rooms you just entered.
	public void RoomWallFadeOut(GameObject room){
		GameObject walls = room.transform.parent.Find("FadingWalls").gameObject;
		walls.GetComponent<FadingWallsScript>().FadeOut();
	}

	// helper function to fade a room's front walls in. Use on rooms you just left.
	public void RoomWallFadeIn(GameObject room){
		GameObject walls = room.transform.parent.Find("FadingWalls").gameObject;
		walls.GetComponent<FadingWallsScript>().FadeIn();
	}

	// helper function to fade a room's front walls out. Use on rooms you just entered.
	public void ActivateDynamicObjects(GameObject room){
		GameObject obj = room.transform.parent.Find("DynamicObjects").gameObject;
		obj.GetComponent<DynamicObject>().ActivateObjects();
	}

	// helper function to fade a room's front walls in. Use on rooms you just left.
	public void DeactivateDynamicObjects(GameObject room){
		GameObject obj = room.transform.parent.Find("DynamicObjects").gameObject;
		obj.GetComponent<DynamicObject>().DeactivateObjects();
	}

	// helper function to add a new room's movable objects to attraction script
	public void UpdateMovableObjects(GameObject room){
		//GameObject obj = room.transform.parent.Find("DynamicObjects").gameObject;
		//this.GetComponent<Attraction>().movable = obj.GetComponent<DynamicObject>().movableObjects;
		this.transform.FindChild("Attraction").GetComponent<Attraction>().movable = room.GetComponent<MovableObject>().movableObjects;
	}
		
}
