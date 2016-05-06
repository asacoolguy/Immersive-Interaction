using UnityEngine;
using System.Collections;

public class TagSetter : MonoBehaviour {
	public string tag_name;
	public int layer = -1;
	Component[] components;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tag_name != "" || layer != -1){
			components = GetComponentsInChildren<Transform>();
			foreach (Component c in components){
				if (tag_name != "")	c.gameObject.tag = tag_name;
				if (layer != -1) c.gameObject.layer = 11;
			}
		}
	}
}
