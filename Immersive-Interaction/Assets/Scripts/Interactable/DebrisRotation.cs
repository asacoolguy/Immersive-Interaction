using UnityEngine;
using System.Collections;

public class DebrisRotation : MonoBehaviour {
	public int rotateAxis;
	public float rotateSpeed = 15f;

	// Use this for initialization
	void Start () {
		// randomly assign a rotation axis
		rotateAxis = Random.Range(0,2);

		// randomize starting rotation
		transform.Rotate(new Vector3(Random.Range(0,359), Random.Range(0,359), Random.Range(0,359)));
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateAxis == 0){
			transform.Rotate(rotateSpeed * Time.deltaTime, 0,0);
		}
		else if(rotateAxis == 1){
			transform.Rotate(0, rotateSpeed * Time.deltaTime,0);
		}
		else{
			transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag != "Ground"){
			//rotateSpeed = 0;
		}

	}
}
