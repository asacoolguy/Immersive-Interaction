using UnityEngine;
using System.Collections;

public class ShadowRemover : MonoBehaviour {
	Component[] components;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		components = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer c in components){
			if (c.gameObject.tag == "Hand"){
				c.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			}
		}
	}
}
