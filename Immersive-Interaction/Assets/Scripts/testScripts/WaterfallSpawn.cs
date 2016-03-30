using UnityEngine;
using System.Collections;

public class WaterfallSpawn : MonoBehaviour {
	public GameObject droplet;
	public float spawnTime; 
	private float timePast;

	// Use this for initialization
	void Start () {
		timePast = Random.Range(spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		timePast += Time.deltaTime;
		if (timePast > spawnTime){
			Instantiate(droplet, gameObject.transform.position, gameObject.transform.rotation);
			timePast = 0;
		}
	}
}
