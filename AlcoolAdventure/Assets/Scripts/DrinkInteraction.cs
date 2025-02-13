using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jug : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = true;
    public bool isEmpty = false;


    private FirstPersonController playerMovement;
    private Animator drinkAnimator;
    [SerializeField] private GameObject handJug;

    void Awake() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        drinkAnimator = playerMovement.GetComponentInChildren<Animator>();
    }

    void Update() {
        //canBeInteractedWith = !isEmpty;
    }

    public void Interact() {
        Drink();
    }

    private void Drink()
    {
        playerMovement.CanMove = false; // Empêche les mouvements de caméra et du joueur
        playerMovement.isDrinking = true; // Passe à l'état "boire"
        handJug.SetActive(!handJug.activeSelf); // Fait apparaître la tasse dans la main du joueur
        gameObject.GetComponent<Renderer>().enabled = false;// Fait disparaître la tasse de la table

        // Démarre la coroutine pour attendre la fin de l'animation
        StartCoroutine(WaitForEndOfAnimation("ToIdle"));
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
        gameObject.GetComponent<Renderer>().enabled = true;
        handJug.SetActive(!handJug.activeSelf);
        isEmpty = true;
        GameLogic.acte += 1;
        playerMovement.isDrinking = false;
        playerMovement.CanMove = true;
    }

    public string GetDescription() {
        return "Boire";
    }
}