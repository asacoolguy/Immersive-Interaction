using UnityEngine;
using System.Collections;

public class EventManagerScript : MonoBehaviour {
	// event that's called for when you enter a new room
	public delegate void EnterRoom(GameObject room);
	public static event EnterRoom OnRoomEnter;

	// event that's called for when player gets destroyed
	public delegate void PlayerDeath(GameObject respawnArea);
	public static event PlayerDeath OnPlayerDeath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enterRoom(GameObject obj){
		if (OnRoomEnter != null){
			OnRoomEnter(obj);
		}
	}

	public void playerDeath(GameObject obj){
		if (OnPlayerDeath != null){
			OnPlayerDeath(obj);
		}
	}
}
