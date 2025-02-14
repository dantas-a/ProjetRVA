using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class NPC : MonoBehaviour
{
    [System.Serializable] public class DialogueInteraction
    {
        public List<string> speakerNames; // Liste des noms qui parlent
        public List<string> dialogueLines; // Liste des répliques
        public List<AudioClip> dialogueVoices; // Liste des fichiers audio associes
        public string endInteractionEvent; // Nom de l’événement déclenché à la fin de l’interaction
    }
    public List<DialogueInteraction> interactions;
    private int dialogueIndex = 0;
    private int interactionIndex = 0;

    [System.Serializable] public class DialogueTrigger
    {
        public string eventName; // Nom de l’événement
        public int newDialogueIndex; // Nouvel index du dialogue
    }

    public List<DialogueTrigger> triggers; // Liste des événements qui affectent ce PNJ

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (var trigger in triggers)
        {
            EventSystemManager.Instance.SubscribeToEvent(trigger.eventName, () => ChangeInteraction(trigger.newDialogueIndex));
        }
    }


    public string GetNameNPCTalking(){
        return interactions[interactionIndex].speakerNames[dialogueIndex];
    }

    public string GetDialogue(){
        return interactions[interactionIndex].dialogueLines[dialogueIndex];
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex < interactions[interactionIndex].dialogueLines.Count)
        {
            dialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void PlayDialogueAudio()
    {
        if (audioSource != null && interactions[interactionIndex].dialogueVoices.Count > dialogueIndex)
        {
            AudioClip clip = interactions[interactionIndex].dialogueVoices[dialogueIndex];
            if (clip != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(clip);
            }
        }
    }

    private void ChangeInteraction(int newIndex)
    {
        interactionIndex = newIndex;
        dialogueIndex = 0; // Reset du dialogue au début de la nouvelle interaction
    }

    public bool EndDialogue()
    {   
        Debug.Log(interactions[interactionIndex].dialogueLines.Count);
        Debug.Log(dialogueIndex);
        
        if (dialogueIndex == interactions[interactionIndex].dialogueLines.Count){
            string eventToTrigger = interactions[interactionIndex].endInteractionEvent;
            Debug.Log("fin dialogue");

            if (!string.IsNullOrEmpty(eventToTrigger))
            {
                EventSystemManager.Instance.TriggerEvent(eventToTrigger);
            }
                dialogueIndex = 0; // Remettre le dialogue à zéro pour la prochaine fois
                return true;
        }
        return false;
    }

}
