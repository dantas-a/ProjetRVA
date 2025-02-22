using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject destination;
    [SerializeField] GameObject playerg;
    private FirstPersonController playerMovement;
    
    private Animator characterAnimator;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    void OnTriggerEnter(Collider other) {
    	if(other.CompareTag("Player")) {
    		playerg.SetActive(false);
            EventSystemManager.Instance.TriggerEvent("TP");
    		playerg.transform.position = destination.transform.position;
    		playerg.SetActive(true);
    	}
    }
}
