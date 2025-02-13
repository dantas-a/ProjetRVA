using UnityEngine;

public class Jug : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = true;
    public bool isEmpty = false;

    private FirstPersonController playerMovement;
    [SerializeField] private Animator drinkAnimator;
    [SerializeField] private GameObject handJug;

    void Awake() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

    }

    void Update() {
        
    }

    public void Interact() {
        Drink();
    }

    private void Drink(){
        playerMovement.CanMove = false; // empêche les mouvements de caméra et du joueur
        handJug.SetActive(!handJug.activeSelf); //Fait apparaître la tasse dans la main du joueur
        gameObject.SetActive(!gameObject.activeSelf); // Fait disparaître la tasse de la table
        //trigger animator (trouver mettre tout dans le même animator) (à faire)
        gameObject.SetActive(!gameObject.activeSelf);
        handJug.SetActive(!handJug.activeSelf);
        isEmpty = true;
        GameLogic.acte += 1;
        playerMovement.CanMove = true;
    }

    public string GetDescription() {
        return "Boire";
    }
}