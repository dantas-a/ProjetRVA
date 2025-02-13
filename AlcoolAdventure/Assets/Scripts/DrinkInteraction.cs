using UnityEngine;

public class Jug : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = true; //nécessite d'être en public car hérite de la propriété de l'interface (??)
    public bool isEmpty = false;

    void Awake() {
        
    }

    void Update() {
        
    }

    public void Interact() {
    
    }

    public string GetDescription() {
        return "Boire";
    }
}