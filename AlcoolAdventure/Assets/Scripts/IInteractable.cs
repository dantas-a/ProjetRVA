using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{   
    bool canBeInteractedWith { get; set; }
    void Interact();
    string GetDescription();
}
