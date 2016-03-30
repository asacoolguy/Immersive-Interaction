using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpeechBubbleGenerator : MonoBehaviour {
	private GameObject background;
	private GameObject dialogue;
	private RectTransform backgroundTransform;
	private RectTransform dialogueTransform;
	public AudioClip beep;

	// Use this for initialization
	void Start () {
		DeactivateBubble();
	}

	void Awake(){
		background = transform.Find("Background").gameObject;
		dialogue = transform.Find("Dialogue").gameObject;
		backgroundTransform = background.GetComponent<RectTransform>();
		dialogueTransform = dialogue.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 curPos = transform.position;
		//transform.eulerAngles = new Vector3(0,180 + Mathf.Atan2(curPos.z, curPos.x) * Mathf.Rad2Deg,0);
	}

	// activates the speech bubbles
	private void ActivateBubble(){
		background.SetActive(true);
		dialogue.SetActive(true);
	}

	// deactivates the speech bubbles
	private void DeactivateBubble(){
		background.SetActive(false);
		dialogue.SetActive(false);
	}

	public bool IsActive(){
		return background.activeInHierarchy && dialogue.activeInHierarchy;
	}

	// wrapper for coroutine DisplayText
	public void DisplayText(List<string> texts){
		StartCoroutine(DisplayTextCo(texts));
	}

	// overload the wrapper to also accept single texts
	public void DisplayText(string text){
		List<string> texts = new List<string>();
		texts.Add(text);

		StartCoroutine(DisplayTextCo(texts));
	}

	// displays the text for this amount of time
	IEnumerator DisplayTextCo(List<string> texts){
		ActivateBubble();

		foreach (string text in texts){
			AdjustBubbleSize(text);
			dialogue.GetComponent<Text>().text = text;
			StartCoroutine(PopText());

			yield return new WaitForSeconds(CalcDisplayTime(text));
		}

		DeactivateBubble();
	}

	// adjust the size of children bubbles by counting the text
	private void AdjustBubbleSize(string text){
		int lines = text.Length / 25 + 1;

		Vector2 oldSize = backgroundTransform.sizeDelta;
		backgroundTransform.sizeDelta = new Vector2(oldSize.x, 35 * lines + 60);
		Vector2 oldSize2 = dialogueTransform.sizeDelta;
		dialogueTransform.sizeDelta = new Vector2(oldSize2.x, 35 * lines);

		/*
		Vector3 oldPos = backgroundTransform.anchoredPosition3D;
		backgroundTransform.anchoredPosition3D = new Vector3(oldPos.x, -150f + 50f / 4f * lines, oldPos.z);
		Vector3 oldPos2 = dialogueTransform.anchoredPosition3D;
		dialogueTransform.anchoredPosition3D = new Vector3(oldPos2.x, 26 - 50f / 8f * lines, oldPos2.z);
		*/
	}

	// makes the text pop up 
	IEnumerator PopText(){
		int popHeight = 15;
		float delay = 0.02f;
		GetComponent<AudioSource>().PlayOneShot(beep);

		for (int i = 0; i <= popHeight; i += i + 1){
			Vector3 pos = dialogueTransform.anchoredPosition3D;
			dialogueTransform.anchoredPosition3D = new Vector3(pos.x, pos.y + i, pos.z);
			yield return new WaitForSeconds(delay);
		}
		for (int i = 0; i <= popHeight; i+= i + 1){
			Vector3 pos = dialogueTransform.anchoredPosition3D;
			dialogueTransform.anchoredPosition3D = new Vector3(pos.x, pos.y - i, pos.z);
			yield return new WaitForSeconds(delay);
		}
	}

	private float CalcDisplayTime(string text){
		int lines = text.Length / 20;
		return 2f + lines * 0.75f;
	}
}
