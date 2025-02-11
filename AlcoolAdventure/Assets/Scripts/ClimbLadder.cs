using UnityEngine;

public class ClimbLadder : MonoBehaviour
{  
    void OnTriggerEnter(Collider other) {
    	if(other.CompareTag("Player")) {
    		other.isClibbing = true;
    	}
    }
    
    void OnTriggerExit(Collider other) {
    	if(other.CompareTag("Player")) {
    		other.isClibbing = false;
    	}
    }
}
