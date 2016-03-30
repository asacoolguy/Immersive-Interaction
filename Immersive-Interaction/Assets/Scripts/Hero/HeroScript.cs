using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroScript : MonoBehaviour {
	public GameObject speechBubble;
	public SpeechBubbleGenerator speechBubbleScript;
	private Rigidbody rbody;
	public GameObject environment;

	// event that's called for when you enter a new room
	public delegate void EnterRoom(GameObject room);
	public static event EnterRoom OnRoomEnter;

	// Use this for initialization
	void Start () {
		speechBubble = transform.parent.Find("SpeechBubble").gameObject;
		speechBubbleScript = speechBubble.GetComponent<SpeechBubbleGenerator>();
		rbody = GetComponent<Rigidbody>();
		environment = GameObject.FindGameObjectWithTag("Environment");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// speaks the given text
	public void Speak(List<string> texts){
		speechBubbleScript.DisplayText(texts);
	}

	// speaks the given text
	// overload the wrapper to also accept single texts
	public void Speak(string text){
		List<string> texts = new List<string>();
		texts.Add(text);

		speechBubbleScript.DisplayText(texts);
	}

	// returns if the hero is currently speaking
	public bool IsSpeaking(){
		return speechBubbleScript.IsActive();
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "RoomTrigger"){
			if (OnRoomEnter != null){
				OnRoomEnter(other.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Dialogue"){
			DialogueTrigger trigger = other.gameObject.GetComponent<DialogueTrigger>();
			List<string> dialogue = trigger.dialogue;
			if (!trigger.triggered && dialogue.Count > 0){
				Speak(dialogue);
				trigger.triggered = true;
			}
		}
	}
}
