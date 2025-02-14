using UnityEngine;

public class LadderInteraction : MonoBehaviour, IInteractable
{
    public Ladder lad;
    public bool canBeInteractedWith { get ; set; } = true;

    public void Interact(){
        lad.StartMovement();
    }
    public string GetDescription(){
        if (lad.climbUp) return "Monter l'échelle";
        return "Descendre l'échelle";
    }
}
