using UnityEngine;
using System.Collections;

public class SelectionColor : MonoBehaviour {
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
