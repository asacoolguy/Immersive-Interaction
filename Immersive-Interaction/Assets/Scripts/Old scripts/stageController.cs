using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;

public class stageController : MonoBehaviour {
	public GameObject hero;
	private HeroMovementControls heroMovementControls;
	public List<GameObject> goalQueue;

	// Use this for initialization
	void Start () {
		//StartCoroutine(StartBehavior());
	}

	void Awake(){
		heroMovementControls = hero.GetComponent<HeroMovementControls>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("left ctrl"))
		{
			UnityEngine.VR.InputTracking.Recenter();
		}
	}
	/*
	IEnumerator StartBehavior(){
		yield return new WaitForSeconds(5);

		List<string> dialogue = new List<string>();
		dialogue.Add("Hi there! I'm the main character of this game.");
		dialogue.Add("I'm going to need your help. Can you let me through the door?");
		heroControls.Speak(dialogue);

		while(heroControls.IsSpeaking()){
			yield return new WaitForSeconds(0.5f);
		}

		while(heroControls.canMove == false){
			yield return new WaitForSeconds(0.5f);
		}

		dialogue.Clear();
		dialogue.Add("Thanks a lot!");
		heroControls.Speak(dialogue);

		while(heroControls.IsSpeaking()){
			yield return new WaitForSeconds(0.5f);
		}

		heroControls.AssignGoal(GetNextGoal());
		while(heroControls.HasGoal()){
			// wait while hero has a goal
			yield return new WaitForSeconds(0.5f);
		}

		dialogue.Clear();
		dialogue.Add("Now I shall move over there");
		heroControls.Speak(dialogue);

		while(heroControls.IsSpeaking()){
			yield return new WaitForSeconds(0.5f);
		}
		heroControls.AssignGoal(GetNextGoal());
		while(heroControls.HasGoal()){
			// wait while hero has a goal
			yield return new WaitForSeconds(0.5f);
		}

		dialogue.Clear();
		dialogue.Add("I HAVE ARRIVED!");
		heroControls.Speak(dialogue);
	}

	// returns the next goal in the list
	private GameObject GetNextGoal(){
		GameObject target = goalQueue[0];
		goalQueue.RemoveAt(0);

		return target;
	}*/
}
