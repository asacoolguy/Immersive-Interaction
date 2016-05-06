using UnityEngine;
using System.Collections;
using LMWidgets;
using Leap;

public class DoorControls : BatteryControlledObject {
	public bool currentOpen = false;
	public bool nextOpen = false;

	public float openTime = 1f;
	public float openDistance = -1;

	public GameObject door;
	public GameObject activeLight;
	public GameObject inactiveLight;

	public AudioSource source;
	public float threshold = 0.02f;

	private float closedPos;
	private float openPos;

	private bool playedErrorNoise = false;
	public AudioClip errorSound;

	void Start(){
		door = transform.FindChild("DoorObj").gameObject;
		activeLight = transform.FindChild("DoorLight_Active").gameObject;
		inactiveLight = transform.FindChild("DoorLight_Inactive").gameObject;
		source = GetComponent<AudioSource>();

		closedPos = transform.position.y;
		openPos = closedPos + openDistance;
	}

	void Update()	{
		closedPos = transform.position.y;
		openPos = closedPos + openDistance;

		// opens and closes the door based
		if (activated){
			if (currentOpen){
				// if doors are not at open pos yet
				if (Mathf.Abs(door.transform.position.y - openPos) > threshold){
					door.transform.Translate(new Vector3(0,openDistance * Time.deltaTime / openTime, 0));
				}
				else{
					door.transform.position = new Vector3(door.transform.position.x, openPos, door.transform.position.z);
				}
			}
			else{
				// if doors are not at closed pos yet
				if (Mathf.Abs(door.transform.position.y - closedPos) > threshold){
					door.transform.Translate(new Vector3(0, -openDistance * Time.deltaTime / openTime, 0));
				}
				else{
					door.transform.position = new Vector3(door.transform.position.x, closedPos, door.transform.position.z);
				}
			}
		}

		// controls the color of the light
		activeLight.SetActive(activated);
		inactiveLight.SetActive(!activated);
	}

	void OnTriggerStay(Collider c){
		if (activated && c.gameObject.tag == "Hero"){
			nextOpen = true;
			if (!currentOpen){
				source.Play();
			}
			currentOpen = nextOpen;
		}
		else if (activated == false && c.gameObject.tag == "Hero" && playedErrorNoise == false){
			source.PlayOneShot(errorSound);
			playedErrorNoise = true;
		}
	}

	void OnTriggerExit(Collider c){
		if (activated && c.gameObject.tag == "Hero"){
			nextOpen = false;
			if (currentOpen){
				source.Play();
			}
			currentOpen = nextOpen;
		}
		else if (activated == false && c.gameObject.tag == "Hero"){
			playedErrorNoise = false;
		}
	}
}
