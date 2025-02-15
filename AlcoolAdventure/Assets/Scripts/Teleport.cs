using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject destination;
    [SerializeField] GameObject playerg;
    
    private Animator characterAnimator;

    void OnTriggerEnter(Collider other) {
    	if(other.CompareTag("Player")) {
    		playerg.SetActive(false);
    		playerg.transform.position = destination.transform.position;
    		playerg.SetActive(true);
    	}
    }
}
