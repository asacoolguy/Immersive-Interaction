using UnityEngine;
using System.Collections;
using Leap;

public class CubeRotation : MonoBehaviour {
	private float current = 0.0f;
	private Controller controller = new Controller();
	private Frame startFrame;
	private Vector3 startVec;
	private Hand lhand;
	private Hand rhand;
	private float ls = 0;
	private float rs = 0;
	private float lp = 0;
	private float rp = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 v = new Vector3(0,current * 360.0f,0);
		
		HandList hands = controller.Frame().Hands;
		foreach (Hand h in hands){
			if (h.IsLeft) lhand = h;
			else rhand = h;
		}
		
		if (rhand != null){
			rs = rhand.GrabStrength;
			rp = rhand.PinchStrength;
		}
		
		if (rs == 1){
			if (startFrame == null){
				//startFrame = controller.Frame();
				//startVec = gameObject.transform.eulerAngles;
			}
			else{
				//Vector vec = rhand.Translation(startFrame);
				//Vector3 v = new Vector3(0,vec.x * (360.0f * 0.01f) - startVec.y,0);
				//gameObject.transform.eulerAngles = v;
			}
		}
		else{
			if (startFrame != null){
				startFrame = null;
			}
		}
	}
}
