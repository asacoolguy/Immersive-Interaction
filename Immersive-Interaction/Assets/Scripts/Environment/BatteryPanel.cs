using UnityEngine;
using System.Collections;

public class BatteryPanel : MonoBehaviour {
	public GameObject mechanism;
	private BatteryControlledObject controlledObj;

	public float insertTime = 5f;
	public float startingPos = 0.15f;
	public float endingPos = -1.34f;

	public AudioClip insertSound;
	public AudioClip lockedSound;
	public AudioClip enableSound;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		controlledObj = mechanism.GetComponent<BatteryControlledObject>();
		source = GetComponent<AudioSource>();
		source.loop = true;
		source.clip = insertSound;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Battery"){
			LockBattery(c.gameObject);
		}
	}

	// sockets in the battery onto the panel
	private void LockBattery(GameObject obj){
		obj.GetComponent<DebrisRotation>().enabled = false;
		obj.GetComponent<Rigidbody>().isKinematic = true;
		obj.transform.SetParent(transform);
		obj.transform.localRotation = Quaternion.identity;
		StartCoroutine(InsertBattery(obj));
		source.Play();
	}

	// simulates an insertion animation for the battery
	IEnumerator InsertBattery(GameObject obj){
		obj.transform.localPosition = new Vector3(0, startingPos,0);
		float distance = endingPos - startingPos;

		while(obj.transform.localPosition.y > endingPos){
			obj.transform.Translate(new Vector3(0, distance / insertTime * Time.deltaTime,0));
			yield return null;
		}

		obj.transform.localPosition = new Vector3(0, endingPos,0);
		source.Stop();
		source.PlayOneShot(lockedSound);

		yield return new WaitForSeconds(2);
		controlledObj.setActivated(true);
		source.PlayOneShot(enableSound);
	}
}
