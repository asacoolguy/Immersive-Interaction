using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroMovementControls : MonoBehaviour {
	private SpeechBubbleGenerator speechBubble;
	public GameObject currentGoal;
	private Rigidbody rbody;
	public GameObject environment;

	public bool canMove = false;

	// parameters
	public float MAX_MOVING_SPEED = 0.3f;

	// Use this for initialization
	void Start () {
		
	}

	void Awake (){
		speechBubble = transform.Find("SpeechBubble").gameObject.GetComponent<SpeechBubbleGenerator>();
		rbody = GetComponent<Rigidbody>();

	}

	void CanMove(){
		canMove = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (HasGoal()){
			// move towards it
			Vector3 goalDir = currentGoal.transform.position - transform.position;
			goalDir = Vector3.ClampMagnitude(goalDir, MAX_MOVING_SPEED);

			rbody.AddForce(goalDir);
		}
		else{
			
			rbody.velocity = rbody.velocity * 0.8f;
			if (rbody.velocity.magnitude < 0.05){
				rbody.velocity = Vector3.zero;
			}
			//rbody.velocity = Vector3.zero;
		}
	}

	// speaks the given text
	public void Speak(List<string> texts){
		speechBubble.DisplayText(texts);
	}

	// speaks the given text
	// overload the wrapper to also accept single texts
	public void Speak(string text){
		List<string> texts = new List<string>();
		texts.Add(text);

		speechBubble.DisplayText(texts);
	}

	// returns if the hero currently has a goal
	public bool HasGoal(){
		return currentGoal != null;
	}

	// assigns a new goal to the hero
	public void AssignGoal(GameObject goal){
		currentGoal = goal;
	}

	// returns if the hero is currently speaking
	public bool IsSpeaking(){
		return speechBubble.IsActive();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == currentGoal){
			currentGoal = null;
			print("stopped");
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "RoomTrigger"){
			environment.GetComponent<EnvironmentScript>().RecenterCamera(other.gameObject);
		}
	}
}
