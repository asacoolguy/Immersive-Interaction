using UnityEngine;
using System.Collections;

public class SpeechBubbleStabilizer : MonoBehaviour {
	public GameObject speechBubble;
	public GameObject model;

	// Use this for initialization
	void Start () {
		speechBubble = transform.Find("SpeechBubble").gameObject;
		model = transform.Find("Model").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// lock the position of speech bubble to model
		speechBubble.transform.position = model.transform.position + new Vector3(0,0.07f,0);
	}
}
