using UnityEngine;
using System.Collections;

// class added to things that your hands shouldn't be able to lift

public class NotLiftable : MonoBehaviour {
	bool noLifting = true;
	Rigidbody rbody;
	RaycastHit hit;
	Vector3 downDir = new Vector3(0,-1,0);
	public float dist = 0.001f;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (noLifting){

		}
	}

	void OnCollisionStay(Collision c){
		if (c.gameObject.tag == "Hand"){
			if (grounded()){
				//freezeY(true);
			}
			else{
				//freezeY(false);
			}
		}
	}

	void OnCollisionExit(Collision c){
		if (c.gameObject.tag == "Hand"){
			print ("leave");
			//freezeY(false);
		}
	}

	// function that freezes the y position based on the input
	public void freezeY(bool input){
		if (input){
			rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}
		else{
			rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}

	}

	// returns if the object is grounded
	public bool grounded(){
		if (Physics.Raycast(transform.position,downDir,out hit,dist)){
			if (hit.collider.gameObject.tag == "Ground"){
				return true;
			}
		}
		return false;
	}
	
}
