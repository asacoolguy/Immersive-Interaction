using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingWallsScript : MonoBehaviour {
	public bool fadingIn = true;
	public float fadeTime = 1.5f;
	public List<Material> mats;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform){
			// activate child if it's not active already
			child.gameObject.SetActive(true);

			GameObject obj = child.Find("Wall").gameObject;
			mats.Add(obj.GetComponent<Renderer>().material);
			if (obj.transform.childCount > 0){
				mats.Add(obj.transform.GetChild(0).GetComponent<Renderer>().material);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if we should currently be fading in
		if (fadingIn){
			foreach (Material mat in mats){
				if (mat.color.a < 1f){
					Color c = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + 1f / fadeTime * Time.deltaTime);
					mat.color = c;
				}
			}
		}
		// otherwise we're fading out
		else{
			foreach (Material mat in mats){
				if (mat.color.a > 0){
					Color c = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 1f / fadeTime * Time.deltaTime);
					mat.color = c;
				}
			}
		}
	}

	public void FadeIn(){
		fadingIn = true;
	}

	public void FadeOut(){
		fadingIn = false;
	}
}
