using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jug : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = false;
    private bool isEmpty = true;

    private FirstPersonController playerMovement;
    private Animator drinkAnimator; 
    private CameraMovementCinematic cameraMovementCinematic;

    public string nameTrigger1;
    public string eventToTrigger1;
    public string eventToTrigger2;
    public string eventToTrigger3;
    public string eventToTrigger4;

    private int nbDrunk = 0;

    [SerializeField] private GameObject handJug;


    void Awake() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        drinkAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        cameraMovementCinematic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovementCinematic>();
        
        EventSystemManager.Instance.SubscribeToEvent(nameTrigger1, () => isEmpty = false);
    }

    void Update() {
        canBeInteractedWith = !isEmpty;
    }

    public void Interact() {
        Drink();
        EventSystemManager.Instance.TriggerEvent(ActHandler());
        nbDrunk += 1;
    }

    private void Drink()
    {   
        if (playerMovement == null){
            Debug.Log("Pas de playerMovement");
        }
        if (drinkAnimator == null){
            Debug.Log("pas de drink animator");
        }
        if (cameraMovementCinematic == null){
            Debug.Log("pas de  cameraMovementCinematic");
        }
        playerMovement.CanMove = false; // Empêche les mouvements de caméra et du joueur
        drinkAnimator.SetBool("canMove", false);
        handJug.SetActive(!handJug.activeSelf); // Fait apparaître la tasse dans la main du joueur
        gameObject.GetComponent<Renderer>().enabled = false;// Fait disparaître la tasse de la table
        cameraMovementCinematic.StartMovement();    // Fait bouger la caméra
        
        drinkAnimator.SetBool("isDrinking", true); // Déclenche l'animation de boisson

        StartCoroutine(WaitForEndOfAnimation("ToIdle"));    // Démarre la coroutine pour attendre la fin de l'animation
    }

    private IEnumerator WaitForEndOfAnimation(string stateName)
    {
        // Attend que l'état spécifique soit actif
        while (!drinkAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null; // Attend la prochaine frame
        }

        // Attend que l'animation de cet état soit terminée
        while (drinkAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // Attend la prochaine frame
        }

        // Une fois l'animation terminée, exécute le reste du code
        drinkAnimator.SetBool("isDrinking", false);
        cameraMovementCinematic.StartMovement();
        gameObject.GetComponent<Renderer>().enabled = true;
        handJug.SetActive(!handJug.activeSelf);
        isEmpty = true;
        playerMovement.CanMove = true;
    }

    
    private string ActHandler()
    {
        switch(nbDrunk){
            case 0:
                return eventToTrigger1;
            
            case 1:
                return eventToTrigger2;
            
            case 2:
                return eventToTrigger3;
            
            default:
                return eventToTrigger4;
        }
    }

    public string GetDescription() {
        return "Boire";
    }
}