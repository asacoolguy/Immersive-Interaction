using UnityEngine;
using System.Collections;
using Leap;

public class CubeTranslation : MonoBehaviour {
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
				float dx = vec.x;
				float dy = vec.z;
				float dz = vec.y;
				//print ("change in x is " + vec.x);
				//float dx = vec.x - startPos.x;
				Vector3 v = new Vector3(-dx, -dy, dz);
				gameObject.transform.position = startPos + (v * 0.001f);
			}
		}
		else{
			if (startFrame != null){
				startFrame = null;
			}
		}
	}

	public void Toggle(){
		selected = true;
	}

	public void Deselect(){
		selected = false;
	}

	public bool isSelected(){
		return selected;
	}
}
