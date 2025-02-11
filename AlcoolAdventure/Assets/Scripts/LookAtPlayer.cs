using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform bodyNPC;
    private float targetYRotation;
    
    void Update()
    {
        float lerpSpeed = 2f;
        Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    }

    public void lookat(Vector3 MainCharacterPosition){
        Vector3 direction = MainCharacterPosition - bodyNPC.position;
        targetYRotation = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg;
    }
}
