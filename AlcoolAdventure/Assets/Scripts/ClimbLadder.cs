using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{  
	private Transform chController;
	bool inside = false;
	public float speedUpDown = 3.2f;
	private FirstPersonController FirstPersonController;
	
	void Start(){
		FirstPersonController = GetComponent<FirstPersonController>();
		chController = GetComponent<Transform>();
		inside = false;
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Ladder") {
			FirstPersonController.enabled = false;
			inside = !inside;
		}
	}
	
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Ladder") {
			FirstPersonController.enabled = true;
			inside = !inside;
		}
	}
	
	void Update() {
		if( inside == true && ( Input.GetKey("w")  || Input.GetKey("up")) ) {
			chController.transform.position +=Vector3.up / speedUpDown;
		}
		if( inside == true && ( Input.GetKey("s")  || Input.GetKey("down")) ) {
			chController.transform.position +=Vector3.down / speedUpDown;
		}
	}
}
