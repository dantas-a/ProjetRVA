using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLore : MonoBehaviour
{

    [System.Serializable] public class Trigger
    {
        public string eventName; // Nom de l’événement
        public string eventText; // Nouvel index du dialogue
        public AudioClip audio;
    }

    public List<Trigger> triggers; // Liste des événements qui affectent ce PNJ
    [Header("Canvas & UI Elements")]
    public GameObject transitionCanvas;
    public TextMeshProUGUI narrativeText;

    [Header("Transition Settings")]
    public float typewriterSpeed = 0.06f; // Vitesse d'écriture

    private bool isNarrativeActive = false;
    private bool isTyping = false; // Indique si le texte est en train d'être écrit
    private bool canExit = false;  // Permet de quitter seulement à la fin du texte

    private FirstPersonController playerMovement;
    private AudioSource audioSource;
    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        audioSource = GetComponent<AudioSource>();
        transitionCanvas.SetActive(false);
        foreach (var trigger in triggers)
        {
            EventSystemManager.Instance.SubscribeToEvent(trigger.eventName, () => ShowNarrative(trigger.eventText, trigger.audio));
        }
    }

    private void Update()
    {
        if (isNarrativeActive && canExit && Input.GetKeyDown(KeyCode.E))
        {
            transitionCanvas.SetActive(false);
            isNarrativeActive = false;
        }
    }

    // Méthode publique pour afficher la narration
    public void ShowNarrative(string text, AudioClip audio)
    {
        isNarrativeActive = true;
        StartCoroutine(NarrativeRoutine(text,audio));
        if (text == "Fin"){
            SceneManager.LoadSceneAsync(0);
        }
    }

    private IEnumerator NarrativeRoutine(string text, AudioClip audio)
    {
        // Active le Canvas
        transitionCanvas.SetActive(true);
        playerMovement.Transition = true;
        
        audioSource.PlayOneShot(audio);
        //  Écriture progressive du texte
        yield return StartCoroutine(TypewriterEffect(text));

        // Active la possibilité de quitter (après la fin du texte)
        canExit = true;


        // Attente de la touche E pour quitter
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        transitionCanvas.SetActive(false);
        isNarrativeActive = false;
        playerMovement.Transition = false;
    }

    // Méthode pour écrire le texte lettre par lettre
    private IEnumerator TypewriterEffect(string text)
    {
        isTyping = true;
        narrativeText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }

        isTyping = false;
    }
}
