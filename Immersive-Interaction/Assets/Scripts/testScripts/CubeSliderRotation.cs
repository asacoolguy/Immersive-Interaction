using UnityEngine;
using System.Collections;
using Leap;
using LMWidgets;

public class CubeSliderRotation: DataBinderSlider {
	public float current = 0.0f;
	public float min = 0.0f;
	public float max = 1.0f;
	private Controller controller = new Controller();
	private Frame startFrame;
	private Vector3 startVec;
	//private Hand lhand;
	private Hand rhand;
	//private float ls = 0;
	private float rs = 0;
	//private float lp = 0;
	//private float rp = 0;
	
	void Awake(){
		Mathf.Clamp(current, 0.0f, 1.0f);
		base.Awake();
	}
	
	override public float GetCurrentData(){
		return current;
	}
	
	override protected void setDataModel(float value){
		current = value;
	}
	
	// Use this for initialization
	void Start () {
		current = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = new Vector3(0,current * 360.0f,0);
		
		gameObject.transform.eulerAngles = v;
		
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
		
		if (rs == 1){
			if (startFrame == null){
				startFrame = controller.Frame();
				startVec = gameObject.transform.eulerAngles;
			}
			else{
				Vector vec = rhand.Translation(startFrame);
				v = new Vector3(0,vec.x * (360.0f * 0.01f) - startVec.y,0);
				gameObject.transform.eulerAngles = v;
			}
		}
		else{
			if (startFrame != null){
				startFrame = null;
			}
		}
		
	}
}
