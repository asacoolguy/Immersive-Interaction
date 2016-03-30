using UnityEngine;
using System.Collections;
using LMWidgets;
using Leap;

public class DoorControls : BatteryControlledObject {
	private bool open = false;
	public float openTime = 1f;
	public float openDistance = 0.04f;

	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject light;
	public Material activeLight;
	public Material inactiveLight;


	private float leftClosedPos;
	private float rightClosedPos;
	private float leftOpenPos;
	private float rightOpenPos;

	void Start(){
		leftDoor = transform.FindChild("left_door").gameObject;
		rightDoor = transform.FindChild("right_door").gameObject;
		light = transform.FindChild("Light").gameObject;

		leftClosedPos = leftDoor.transform.position.z;
		rightClosedPos = rightDoor.transform.position.z;
		leftOpenPos = leftClosedPos + openDistance;
		rightOpenPos = rightClosedPos - openDistance;
	}

	void Update()	{
		// opens and closes the door based
		if (activated){
			if (open){
				// if doors are not at open pos yet
				if (Mathf.Abs(leftDoor.transform.position.z - leftOpenPos) > 0.005f){
					leftDoor.transform.Translate(new Vector3(0,0, openDistance * Time.deltaTime / openTime));
					rightDoor.transform.Translate(new Vector3(0,0, -openDistance * Time.deltaTime / openTime));
				}
				else{
					leftDoor.transform.position = new Vector3(leftDoor.transform.position.x, leftDoor.transform.position.y, leftOpenPos);
					rightDoor.transform.position = new Vector3(rightDoor.transform.position.x, rightDoor.transform.position.y, rightOpenPos);
				}
			}
			else{
				// if doors are not at closed pos yet
				if (Mathf.Abs(leftDoor.transform.position.z - leftClosedPos) > 0.005f){
					leftDoor.transform.Translate(new Vector3(0,0, -openDistance * Time.deltaTime / openTime));
					rightDoor.transform.Translate(new Vector3(0,0, openDistance * Time.deltaTime / openTime));
				}
				else{
					leftDoor.transform.position = new Vector3(leftDoor.transform.position.x, leftDoor.transform.position.y, leftClosedPos);
					rightDoor.transform.position = new Vector3(rightDoor.transform.position.x, rightDoor.transform.position.y, rightClosedPos);
				}
			}
		}

		// controls the color of the light
		if (activated){
			light.GetComponent<Renderer>().material = activeLight;
		}
		else{
			light.GetComponent<Renderer>().material = inactiveLight;
		}

	}

	void OnTriggerEnter(Collider c){
		if (activated && c.gameObject.tag == "Hero"){
			open = true;
		}
	}

	void OnTriggerExit(Collider c){
		if (activated && c.gameObject.tag == "Hero"){
			open = false;
		}
	}
}
