using UnityEngine;
using System.Collections;
using Leap;

public class BoxSelection : MonoBehaviour {
	public GameObject sphere;
	private Controller controller = new Controller();
	public HandController handController;
	private Frame startFrame;
	private Hand rhand;
	private float rs = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// assign hands
		HandList hands = controller.Frame().Hands;
		foreach (Hand h in hands){
			if (h.IsRight) rhand = h;
		}

		if (rhand != null){
			
		}

		// get palm position and direction
		if (rhand != null){
			rs = rhand.GrabStrength;

			if (rs < 1){
				//Vector3 di = rhand.PalmNormal.ToUnity(false);
				//Vector3 direction = handController.transform.TransformDirection(di) * 360.0f;
				Vector3 localPos = rhand.PalmPosition.ToUnityScaled(false);
				Vector3 position = handController.transform.TransformPoint(localPos);
				Vector3 direction = position.normalized;

				//sphere.SetActive(false);
				foreach(Transform child in transform.Find("cubes")){
					child.gameObject.GetComponent<CubeTranslation>().Deselect();
				}

				//bit shift the index of the layer 8 to get a bit mask
				int layerMask = 1 << 12;

				RaycastHit hit;
				if (Physics.Raycast(position, direction, out hit, Mathf.Infinity, layerMask)){
					//sphere.SetActive(true);
					//sphere.transform.position = hit.point;
					print(hit.collider.gameObject.name);
					//print(layerMask);
					hit.collider.gameObject.GetComponent<CubeTranslation>().Select();
				}
			}


		}

	}
}
