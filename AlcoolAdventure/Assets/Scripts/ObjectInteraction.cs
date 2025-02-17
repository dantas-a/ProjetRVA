using UnityEngine;

public class ObjectInteraction : MonoBehaviour, IInteractable
{
    public bool canBeInteractedWith { get; set; } = false;

    private GameEventTrigger gameEventTrigger;
    public string nameTrigger;
    public string description;

    private void Start()
    {
        EventSystemManager.Instance.SubscribeToEvent(nameTrigger, () => canBeInteractedWith = true);
        gameEventTrigger = GetComponent<GameEventTrigger>();
    }
    
    public void Interact() {
        gameEventTrigger.TriggerEvent();
        Destroy(gameObject);
        
    }

    public string GetDescription() {
        return description;
    }
}
