using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroScript : MonoBehaviour {
	public GameObject speechBubble;
	public SpeechBubbleGenerator speechBubbleScript;
	private Rigidbody rbody;
	public GameObject environment;
	public EventManagerScript eventManager;
	public GameObject respawnArea;


	// Use this for initialization
	void Start () {
		speechBubble = transform.parent.Find("SpeechBubble").gameObject;
		speechBubbleScript = speechBubble.GetComponent<SpeechBubbleGenerator>();
		rbody = GetComponent<Rigidbody>();
		environment = GameObject.FindGameObjectWithTag("Environment");
		eventManager = environment.GetComponent<EventManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)){
			PlayerDeath();
		}
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
			eventManager.enterRoom(other.gameObject);
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
		else if (other.gameObject.tag == "Respawn"){
			respawnArea = other.gameObject;
		}
		else if (other.gameObject.tag == "Finish"){
			// game is done

		}
		else if (other.gameObject.tag == "Danger"){
			PlayerDeath();
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag == "Danger"){
			PlayerDeath();
		}
	}

	public void PlayerDeath(){
		eventManager.playerDeath(respawnArea);
		//StartCoroutine(Suicide());
		Destroy(transform.parent.gameObject);
	}

	IEnumerator Suicide(){
		yield return new WaitForSeconds(0.5f);
		Destroy(transform.parent.gameObject);
	}
}
