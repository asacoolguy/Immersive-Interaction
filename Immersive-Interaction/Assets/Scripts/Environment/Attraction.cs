using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class Attraction : MonoBehaviour {
	public HandController handController;
	private Controller controller = new Controller();
	private Frame startFrame;
	private Hand rhand;
	private Hand lhand;
	private float ls = 0;
	private float rs = 0;

	public AudioClip chargingUp;
	public AudioSource source;
	public bool playingAudio = false;

	public float moveForce = 1f;
	public List<GameObject> movable;

	public GameObject attractorFX;

	// Use this for initialization
	void Start () {
		movable = new List<GameObject>();
		if (GameObject.FindGameObjectWithTag("Hero") != null){
			movable.Add(GameObject.FindGameObjectWithTag("Hero"));
		}
			
		source = this.GetComponent<AudioSource>();
		playingAudio = false;

		attractorFX = transform.GetChild(0).gameObject;
		attractorFX.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		// make sure hero is in the list
		if (GameObject.FindGameObjectWithTag("Hero") != null){
			GameObject o = GameObject.FindGameObjectWithTag("Hero");
			if (!movable.Contains(o)){
				movable.Add(o);
			}
		}

		// assign hands
		HandList hands = controller.Frame().Hands;
		foreach (Hand h in hands){
			if (h.IsRight) rhand = h;
			else lhand = h;
		}

		// get palm position and direction
		if (rhand != null){
			rs = rhand.GrabStrength;

			if (rs == 1){
				//print("attracting");
				// attract hero object towards hand
				Vector3 localPos = rhand.PalmPosition.ToUnityScaled(false);
				Vector3 position = handController.transform.TransformPoint(localPos);

				foreach(GameObject obj in movable){
					if (obj == null){
						continue;
					}

					Vector3 force = (position - obj.transform.position) * moveForce;

					if (obj.gameObject.tag == "Hero"){
						force *= 3;
					}
					obj.GetComponent<Rigidbody>().AddForce(force);
				}

				if (playingAudio == false){
					ChargeAudioStart();
				}

				attractorFX.SetActive(true);
				attractorFX.transform.position = position;
			}
			else{
				//print("not attracting");
				ChargeAudioStop();
				attractorFX.SetActive(false);
			}

		}
	}

	public void  ChargeAudioStart()
	{
		source.pitch = Random.Range(0.8f, 1f);
		source.volume = Random.Range(0.8f, 1f);
		playingAudio = true;

		source.PlayOneShot(chargingUp);
	}

	public void ChargeAudioStop(){
		this.GetComponent<AudioSource>().Stop();
		playingAudio = false;
	}
}
