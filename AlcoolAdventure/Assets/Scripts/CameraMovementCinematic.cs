using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementCinematic : MonoBehaviour
{
    // Gestion Caméra
    private bool leavePlayer = true;
    private float moveDuration = 1.0f;
    private float rotationSpeed = 5.0f;


    private Quaternion rotationBody;
    private Vector3 positionBody;
    private Vector3 positionCinematic;
    private Quaternion rotationCinematic;

    private IEnumerator MoveCamera()
    {
        if (leavePlayer) {
            // Déplacer vers la position en bas
            rotationBody = transform.rotation;
            positionBody = transform.position;

            rotationCinematic = Quaternion.Euler(30,223,-2);
            positionCinematic = positionBody;
            positionCinematic.x += 2.2f;
            positionCinematic.y += 0.5f;
            positionCinematic.z += 0.8f; 

            yield return MoveToTarget(positionCinematic, rotationCinematic);


        } else {
            // Déplacer vers la position en haut
            yield return MoveToTarget(positionBody, rotationBody);

        }
        leavePlayer = !leavePlayer;
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

    public void StartMovement()
    {
        StartCoroutine(MoveCamera());
    }


}
