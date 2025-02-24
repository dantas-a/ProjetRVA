using UnityEngine;

public class HandlePuke : MonoBehaviour
{
    private ParticleSystem pukeEffect;
    public GameObject farmer;
    public float activationDistance = 3f;

    public GameObject pukeCanvas;

    private bool canPuke = false;
    private float distanceToFarmer;

    void Start()
    {
        pukeEffect = GetComponentInChildren<ParticleSystem>();

        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pegu 0", () => canPuke = true);
        pukeCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        distanceToFarmer = Vector3.Distance(transform.position, farmer.transform.position);
        
        DisplayCanvas();
        Puke();
    }

    private void Puke()
    {
        if ((Input.GetKeyDown(KeyCode.V)) && (canPuke == true))
        {
            pukeEffect.Play();
            if (distanceToFarmer <= activationDistance)
            {
                EventSystemManager.Instance.TriggerEvent("Vomit");
            }
        }
    }

    private void DisplayCanvas()
    {
        if((distanceToFarmer <= activationDistance) && canPuke)
        {
            pukeCanvas.gameObject.SetActive(true);
        }
        else
        {
            pukeCanvas.gameObject.SetActive(false);
        }
    }
}