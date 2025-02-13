using UnityEngine;

public class UpstairsDoor : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = true;
    public float smooth = 10f; // Vitesse de rotation
    private float targetYRotation; // Rotation cible autour de l'axe Y
    private float defaultYRotation; // Rotation par défaut (position fermée)
    private bool isOpen; // État de la porte

    // Décalage du pivot local (coin en bas à gauche de la porte)
    public Vector3 pivotOffset = new Vector3(0f, 0f, 0f); // Ajustez selon la taille de la porte

    void Start() {
        // Enregistre la rotation initiale de la porte
        defaultYRotation = transform.eulerAngles.y;
        targetYRotation = defaultYRotation;
    }

    void Update() {
        // Applique la rotation en douceur
        Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);
    }

    public void ToggleDoor() {
        isOpen = !isOpen;

        if (isOpen) {
            // Ouvre la porte de 90 degrés
            targetYRotation = defaultYRotation - 133.1f;
        } else {
            // Ferme la porte (retour à la rotation par défaut)
            targetYRotation = defaultYRotation;
        }
    }

    public void Interact() {
        ToggleDoor();
    }

    public string GetDescription() {
        if (isOpen) return "Fermer la porte";
        return "Ouvrir la porte";
    }
}