using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    public string eventToTrigger; // Nom de l'événement à déclencher

    public void TriggerEvent()
    {
        EventSystemManager.Instance.TriggerEvent(eventToTrigger);
    }
}
