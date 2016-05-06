using UnityEngine;
using System.Collections;

public class GravityField : MonoBehaviour {
	public enum Direction{
		up,
		down,
		left,
		right
	};
		
	public float speed;
	public Direction direction;
	public Vector3 movementVector;
	public Material upMat;
	public Material downMat;

	// Use this for initialization
	void Start () {
		if (direction == Direction.up){
			movementVector = new Vector3(0,1,0);
			GetComponent<Renderer>().material = upMat;
		}
		else if(direction == Direction.down){
			movementVector = new Vector3(0,-1,0);
			GetComponent<Renderer>().material = downMat;
		}
		else if(direction == Direction.left){
			movementVector = new Vector3(-1,0,0);
			GetComponent<Renderer>().material = downMat;
		}
		else if(direction == Direction.down){
			movementVector = new Vector3(1,01,0);
			GetComponent<Renderer>().material = downMat;
		}
		movementVector *= speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Movable"){
			c.gameObject.transform.position += movementVector;
		}
	}
}
