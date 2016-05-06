using UnityEngine;
using System.Collections;

public class FlickeringLights : MonoBehaviour {
	public bool flicker = false;
	public float flickerInterval = 4f; // interval between each flickering session
	public float currentInterval = 0f;
	public float flickerOffDuration = 0.04f; // interval lights stays off during flicker
	public float flickerOnDuration = 0.02f; // interval lights stays on during flicker
	public bool lightOn = true;
	public int flickerAmount = 2; // number of times the light flickers every session

	public Material onMaterial;
	public Material offMaterial;

	private GameObject light;
	private Renderer model;

	// Use this for initialization
	void Start () {
		light = this.transform.Find("light").gameObject;
		model = this.transform.Find("model").gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (lightOn){
			light.SetActive(true);
			model.material = onMaterial;
		}
		else{
			light.SetActive(false);
			model.material = offMaterial;
		}

		if (flicker){
			if (currentInterval < flickerInterval){
				currentInterval += Time.deltaTime;
			}
			else{
				currentInterval = 0f;
				StartCoroutine(FlickerLight());
			}
		}
	}

	IEnumerator FlickerLight(){
		for(int i = 0; i < flickerAmount; i++){
			lightOn = false;
			yield return new WaitForSeconds(Random.Range(flickerOffDuration / 2f, flickerOffDuration * 3f / 2f));
			lightOn = true;
			yield return new WaitForSeconds(Random.Range(flickerOnDuration / 2f, flickerOnDuration * 3f / 2f));
		}
	}
}
