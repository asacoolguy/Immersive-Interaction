using UnityEngine;
using System.Collections;

public class WaterfallDestory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerEnter(Collider other){
		print("yoooo");
		if (other.gameObject.tag == "droplets"){
			Destroy(other.gameObject);
		}
	}


		
}
