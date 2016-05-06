using UnityEngine;
using System.Collections;
using Leap;

public class CubeTranslation : MonoBehaviour {
	public bool xTranslation;
	public bool yTranslation;
	public bool zTranslation;

	public GameObject leftMoveLimit;
	public GameObject rightMoveLimit;
	public GameObject frontMoveLimit;
	public GameObject backMoveLimit;
	public GameObject topMoveLimit;
	public GameObject botMoveLimit;


	private Controller controller = new Controller();
	private Frame startFrame;
	private Vector3 startPos;
	//private Hand lhand;
	private Hand rhand;
	//private float ls = 0;
	private float rs = 0;
	//private float lp = 0;
	//private float rp = 0;
	public Material on;
	public Material off;
	private bool selected = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (selected){
			gameObject.GetComponent<Renderer>().material = on;
		}
		else{
			gameObject.GetComponent<Renderer>().material = off;
		}

		// assign hands
		HandList hands = controller.Frame().Hands;
		foreach (Hand h in hands){
			//if (h.IsLeft) lhand = h;
			//else rhand = h;
			if (h.IsRight) rhand = h;
		}

		if (rhand != null){
			rs = rhand.GrabStrength;
			//rp = rhand.PinchStrength;
		}

		if (rs == 1 && isSelected()){
			if (startFrame == null){
				startFrame = controller.Frame();
				startPos = gameObject.transform.position;
			}
			else{
				Vector vec = rhand.Translation(startFrame);
				float dx, dy, dz;
				dx = dy = dz = 0f;
				print(transform.position.x);
				print(rightMoveLimit.transform.position.x);
				print(leftMoveLimit.transform.position.x);
				if (xTranslation
					&& transform.position.x <= rightMoveLimit.transform.position.x
					&& transform.position.x >= leftMoveLimit.transform.position.x){
					dx = -vec.x;
				}
				if (yTranslation
					&& transform.position.y <= topMoveLimit.transform.position.y
					&& transform.position.y >= botMoveLimit.transform.position.y){
					dy = -vec.z;
				}
				if (zTranslation
					&& transform.position.z >= frontMoveLimit.transform.position.z
					&& transform.position.z <= backMoveLimit.transform.position.z){
					dz = vec.y;
				}
				//print ("change in x is " + vec.x);
				//float dx = vec.x - startPos.x;
				Vector3 v = new Vector3(dx, dy, dz);
				transform.position = startPos + (v * 0.01f);
			}
		}
		else{
			if (startFrame != null){
				startFrame = null;
			}
		}

		// set bounds
		if (xTranslation){
			if (transform.position.x > rightMoveLimit.transform.position.x){
				transform.position = new Vector3(rightMoveLimit.transform.position.x, transform.position.y, transform.position.z);
			}
			else if (transform.position.x < leftMoveLimit.transform.position.x){
				transform.position = new Vector3(leftMoveLimit.transform.position.x, transform.position.y, transform.position.z);
			}
		}
		if (yTranslation){
			if (transform.position.y > topMoveLimit.transform.position.y){
				transform.position = new Vector3(transform.position.x, topMoveLimit.transform.position.y, transform.position.z);
			}
			else if (transform.position.y < botMoveLimit.transform.position.y){
				transform.position = new Vector3(transform.position.x, botMoveLimit.transform.position.y, transform.position.z);
			}
		}
		if (zTranslation){
			if (transform.position.z < frontMoveLimit.transform.position.z){
				transform.position = new Vector3(transform.position.x, transform.position.y, frontMoveLimit.transform.position.z);
			}
			else if (transform.position.z > backMoveLimit.transform.position.z){
				transform.position = new Vector3(transform.position.x, transform.position.y, backMoveLimit.transform.position.z);
			}
		}
	}

	public void Select(){
		selected = true;
	}

	public void Deselect(){
		selected = false;
	}

	public bool isSelected(){
		return selected;
	}
}
