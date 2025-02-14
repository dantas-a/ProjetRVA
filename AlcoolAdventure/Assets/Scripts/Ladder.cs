using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    public bool climbUp = true;
    public float smooth = 10f;
    public float moveDuration = 2.0f; // Durée pour chaque mouvement
    public float rotationSpeed = 5.0f; // Vitesse de rotation
    public bool startOnAwake = true; // Si le mouvement commence automatiquement

    [Header("En bas")]
    public Vector3 targetPosition1;
    public Quaternion targetRotation1;

    [Header("En haut")]
    public Vector3 targetPosition2;
    public Quaternion targetRotation2;
    
    public void StartMovement()
    {
        StartCoroutine(MoveSequence());
    }

    private IEnumerator MoveSequence()
    {
        
        if (climbUp) {
            // Déplacer vers la position en bas
            yield return MoveToTarget(targetPosition1, targetRotation1);

            // Déplacer vers la position en haut
            yield return MoveToTarget(targetPosition2, targetRotation2);
            
        } else {
            // Déplacer vers la position en haut
            yield return MoveToTarget(targetPosition2, targetRotation2);

             // Déplacer vers la position en bas
            yield return MoveToTarget(targetPosition1, targetRotation1);

        }
        climbUp = !climbUp;
    }

    private IEnumerator MoveToTarget(Vector3 targetPos, Quaternion targetRot)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Interpolation de la position
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            // Interpolation de la rotation
            transform.rotation = Quaternion.Lerp(startRot, targetRot, t * rotationSpeed);

            yield return null;
        }

        // S'assurer que l'objet atteint exactement la cible
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}
