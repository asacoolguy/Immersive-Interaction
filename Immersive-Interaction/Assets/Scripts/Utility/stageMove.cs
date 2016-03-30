using UnityEngine;
using System.Collections;

public class stageMove : MonoBehaviour {
	public float moveSpeed = 0.005f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = transform.position;

		if (Input.GetKey("w")){
			transform.position = new Vector3(p.x, p.y + moveSpeed, p.z);
		}
		else if(Input.GetKey("s")){
			transform.position = new Vector3(p.x, p.y - moveSpeed, p.z);
		}
		else if(Input.GetKey("a")){
			transform.position = new Vector3(p.x - moveSpeed, p.y, p.z);
		}
		else if(Input.GetKey("d")){
			transform.position = new Vector3(p.x + moveSpeed, p.y, p.z);
		}
	}
}
