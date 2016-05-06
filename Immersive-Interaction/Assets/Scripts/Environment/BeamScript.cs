using UnityEngine;
using System.Collections;

public class BeamScript : TrapScript {
	LineRenderer line;
	public GameObject beam;

	[Header("Rotation")]
	public bool rotate = false;
	public float rotateAngle1 = 0;
	public float rotateAngle2 = 0;
	public float currentAngle = 0;
	public int rotateDirection = 1;
	public float rotateTime = 4f;
	public float rotateWaitTime = 2f;
	public float currentRotateWaitTime = 0f;
	[Header("Moving")]
	public Vector3 initialPos;
	public bool move = false;
	public float movePt1 = 0;
	public float movePt2 = 0;
	public float currentPt = 0;
	public int moveDirection = 1;
	public float moveTime = 4f;
	public float moveWaitTime = 2f;
	public float currentMoveWaitTime = 0f;

	[Header("Audio")]
	public AudioClip beamOpen;
	public AudioClip beamLoop;
	public AudioClip beamClose;
	private AudioSource source;

	// Use this for initialization
	void Start () {/*
		line = GetComponent<LineRenderer>();
		line.enabled = false;
		line.GetComponent<Renderer>().material.color = Color.yellow;
*/
		beam = transform.Find("beam").gameObject;
		foreach(Transform child in beam.transform){
			child.gameObject.SetActive(false);
		}

		currentAngle = 0f;
		initialPos = transform.parent.transform.position;

		source = this.GetComponent<AudioSource>();
		source.clip = beamLoop;
		source.loop = true;
		source.Stop();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		if (rotate){
			if (currentRotateWaitTime == 0){
				if (rotateDirection > 0){
					if (currentAngle < rotateAngle2){
						currentAngle += (rotateAngle2 - rotateAngle1)  / rotateTime * Time.deltaTime;
					}
					else{
						currentAngle = rotateAngle2;
						rotateDirection *= -1;
						currentRotateWaitTime += Time.deltaTime;
					}
				}
				else{
					if (currentAngle > rotateAngle1){
						currentAngle += (rotateAngle1 - rotateAngle2)  / rotateTime * Time.deltaTime;
					}
					else{
						currentAngle = rotateAngle1;
						rotateDirection *= -1;
						currentRotateWaitTime += Time.deltaTime;
					}
				}
			}
			else if(currentRotateWaitTime < rotateWaitTime){
				currentRotateWaitTime += Time.deltaTime;
			}
			else{
				currentRotateWaitTime = 0;
			}

			transform.localEulerAngles = new Vector3(0,0,currentAngle);
		}

		initialPos = transform.parent.transform.position;
		if (move){
			if (currentMoveWaitTime == 0){
				if (moveDirection > 0){
					if (currentPt < movePt2){
						currentPt += (movePt2 - movePt1)  / moveTime * Time.deltaTime;
					}
					else{
						currentPt = movePt2;
						moveDirection *= -1;
						currentMoveWaitTime += Time.deltaTime;
					}
				}
				else{
					if (currentPt > movePt1){
						currentPt += (movePt1 - movePt2)  / moveTime * Time.deltaTime;
					}
					else{
						currentPt = movePt1;
						moveDirection *= -1;
						currentMoveWaitTime += Time.deltaTime;
					}
				}
			}
			else if(currentMoveWaitTime < moveWaitTime){
				currentMoveWaitTime += Time.deltaTime;
			}
			else{
				currentMoveWaitTime = 0;
			}

			transform.position = new Vector3
				(initialPos.x + currentPt,transform.position.y,transform.position.z);
		}
	}

	public override void EnableTrap(){
		foreach(Transform child in beam.transform){
			child.gameObject.SetActive(true);
		}

		BeamAudioLoop();
	}

	public override void DisableTrap(){
		foreach(Transform child in beam.transform){
			child.gameObject.SetActive(false);
		}

		BeamAudioClose();
	}

	public void BeamAudioLoop()
	{
		source.pitch = Random.Range(0.8f, 1f);
		source.volume = Random.Range(0.8f, 1f);
		source.PlayOneShot(beamOpen);

		source.loop = true;
		source.Play();
	}

	public void BeamAudioClose(){
		source.loop = false;
		source.Stop();

		source.pitch = Random.Range(0.8f, 1f);
		source.volume = Random.Range(0.8f, 1f);
		source.PlayOneShot(beamClose);
	}
}
