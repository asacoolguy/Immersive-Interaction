using UnityEngine;
using System.Collections;

public class TagSetter : MonoBehaviour {
	public string tag_name;
	Component[] components;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tag_name != ""){
			components = GetComponentsInChildren<Transform>();
			foreach (Component c in components){
				c.gameObject.tag = tag_name;
			}
		}
	}
}
