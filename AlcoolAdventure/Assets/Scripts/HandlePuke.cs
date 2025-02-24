using UnityEngine;

public class HandlePuke : MonoBehaviour
{
    private ParticleSystem pukeEffect;
    public GameObject farmer;
    public float activationDistance = 3f;

    public GameObject pukeCanvas;

    private bool canPukeCanvas = false;
    private bool canPuke = false;
    private float distanceToFarmer;

    void Start()
    {
        pukeEffect = GetComponentInChildren<ParticleSystem>();

        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pegu 1", () => canPuke = true);
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pegu 1", () => canPukeCanvas = true);
        EventSystemManager.Instance.SubscribeToEvent("Vomit", () => canPukeCanvas = false);
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
        if((distanceToFarmer <= activationDistance) && canPukeCanvas)
        {
            pukeCanvas.gameObject.SetActive(true);
        }
        else
        {
            pukeCanvas.gameObject.SetActive(false);
        }
    }
}