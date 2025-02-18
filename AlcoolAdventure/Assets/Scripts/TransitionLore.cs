using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransitionLore : MonoBehaviour
{
    [Header("Canvas & UI Elements")]
    public CanvasGroup narrativeCanvasGroup;
    public TextMeshProUGUI narrativeText;

    [Header("Transition Settings")]
    public float fadeDuration = 1.0f;
    public float typewriterSpeed = 0.06f; // Vitesse d'écriture

    private bool isNarrativeActive = false;
    private bool isTyping = false; // Indique si le texte est en train d'être écrit
    private bool canExit = false;  // Permet de quitter seulement à la fin du texte

    private void Start()
    {
        // Cacher le Canvas au départ
        narrativeCanvasGroup.alpha = 0;
        narrativeCanvasGroup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNarrativeActive && canExit && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CloseNarrative());
        }
    }

    // Méthode publique pour afficher la narration
    public void ShowNarrative(string text)
    {
        StartCoroutine(NarrativeRoutine(text));
    }

    private IEnumerator NarrativeRoutine(string text)
    {
        // Active le Canvas
        narrativeCanvasGroup.gameObject.SetActive(true);

        // 🟡 Transition Entrée (Fade In)
        yield return StartCoroutine(FadeCanvas(narrativeCanvasGroup, 0, 1, fadeDuration));

        // 🖋️ Écriture progressive du texte
        yield return StartCoroutine(TypewriterEffect(text));

        // Active la possibilité de quitter (après la fin du texte)
        canExit = true;

        // Attente de la touche E pour quitter
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        // 🔴 Transition Sortie (Fade Out)
        yield return StartCoroutine(CloseNarrative());
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

    // Méthode pour fermer la narration
    private IEnumerator CloseNarrative()
    {
        canExit = false;
        yield return StartCoroutine(FadeCanvas(narrativeCanvasGroup, 1, 0, fadeDuration));
        narrativeCanvasGroup.gameObject.SetActive(false);
        isNarrativeActive = false;
    }

    // Transition (Fade)
    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
