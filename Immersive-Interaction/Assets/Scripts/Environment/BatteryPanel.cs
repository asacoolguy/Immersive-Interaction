using UnityEngine;
using System.Collections;

public class BatteryPanel : MonoBehaviour {
	public GameObject mechanism;
	private BatteryControlledObject controlledObj;

	// Use this for initialization
	void Start () {
		controlledObj = mechanism.GetComponent<BatteryControlledObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Battery"){
			controlledObj.setActivated(true);
			LockBattery(c.gameObject);
		}
	}

	// sockets in the battery onto the panel
	private void LockBattery(GameObject obj){
		obj.GetComponent<DebrisRotation>().enabled = false;
		obj.GetComponent<Rigidbody>().isKinematic = true;
		obj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
		obj.transform.eulerAngles = Vector3.zero;
		obj.transform.SetParent(transform);
	}
}
