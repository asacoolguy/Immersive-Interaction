using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderingSparks : MonoBehaviour {
	public GameObject spark;
	public float lowerY;
	public float higherY;
	public float wanderDistance;
	public float wanderTime = 1f;
	public int[] direction;
	public List<GameObject> sparks;
	public bool reactor = false;

	// Use this for initialization
	void Start () {
		spark = transform.Find("Sparks").gameObject;

		wanderDistance = transform.Find("Tank").localScale.y * 8f;
		if (reactor){
			wanderDistance = transform.Find("Tank").localScale.y / 2f;
		}

		lowerY = spark.transform.position.y - wanderDistance;
		higherY = spark.transform.position.y + wanderDistance;

		direction = new int[spark.transform.childCount];
		for (int i = 0; i < spark.transform.childCount; i++){
			int r = Random.Range(1,3);
			if (r == 1){
				direction[i] = -1;
			}
			else{
				direction[i] = 1;
			}

		}

		sparks = new List<GameObject>();
		foreach(Transform child in spark.transform){
			sparks.Add(child.gameObject);
			child.transform.position = new Vector3
				(child.transform.position.x, Random.Range(lowerY,higherY), child.transform.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		lowerY = spark.transform.position.y - wanderDistance;
		higherY = spark.transform.position.y + wanderDistance;
	
		for(int i = 0; i < sparks.Count; i++){
			sparks[i].transform.Translate(
				new Vector3(0,wanderDistance / wanderTime * Time.deltaTime * direction[i],0)
			);

			if ((direction[i] > 0 && sparks[i].transform.position.y > higherY)
				|| (direction[i] < 0 && sparks[i].transform.position.y < lowerY)){
				direction[i] *= -1;
			}
		}
	}
}
