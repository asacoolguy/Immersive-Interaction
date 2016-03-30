using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechTyper : MonoBehaviour {
	public string message;
	private Text textComp;
	public float startDelay = 2f;
	public float typeDelay = 0.01f;
	public AudioClip beep;


	// Use this for initialization
	void Start () {
		StartCoroutine("TypeIn");
	}

	void Awake(){
		textComp = GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator TypeIn(){
		yield return new WaitForSeconds(startDelay);

		for(int i = 0; i < message.Length; i++){
			textComp.text = message.Substring(0,i);
			GetComponent<AudioSource>().PlayOneShot(beep);
			yield return new WaitForSeconds(typeDelay);
		}
	}
}
