using UnityEngine;

public class Coffin : MonoBehaviour, IInteractable {

    public bool canBeInteractedWith { get; set; } = true;
    public float smooth = 10f; // Vitesse de rotation
    private float targetXRotation; // Rotation cible autour de l'axe X
    private float defaultXRotation; // Rotation par défaut (position fermée)
    private bool isOpen; // État de la porte

    public float rotationAmount;

    // Décalage du pivot local
    public Vector3 pivotOffset = new Vector3(0f, 0f, 0f);

    void Start() {
        // Enregistre la rotation initiale
        defaultXRotation = transform.eulerAngles.x;
        targetXRotation = defaultXRotation;
    }

    void Update() {
        // Applique la rotation en douceur
        Quaternion targetRotation = Quaternion.Euler(targetXRotation, 0f, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);
    }

    public void ToggleDoor() {
        isOpen = !isOpen;

        if (isOpen) {
            // Ouvre le cercueil de 90 degrés
            targetXRotation = defaultXRotation - rotationAmount;
        } else {
            // Ferme le cercueil (retour à la rotation par défaut)
            targetXRotation = defaultXRotation;
        }
    }

    public void Interact() {
        ToggleDoor();
    }

    public string GetDescription() {
        if (isOpen) return "Fermer le cercueil";
        return "Ouvrir le cercueil";
    }
}