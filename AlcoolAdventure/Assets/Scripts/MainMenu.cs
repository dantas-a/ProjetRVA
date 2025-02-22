using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{       
    public GameObject menuCanvas;
    public GameObject transitionCanvas;
    public TextMeshProUGUI narrativeText;
    public AudioClip clip;
    private AudioSource audioSource;
    public float typewriterSpeed = 0.06f; // Vitesse d'écriture
    private bool canExit = false;  // Permet de quitter seulement à la fin du texte
    public string intro;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canExit && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadSceneAsync(1);
            transitionCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            
        }
    }
    public void PlayGame() {
        menuCanvas.SetActive(false);
        StartCoroutine(NarrativeRoutine());
    }
    private IEnumerator NarrativeRoutine()
    {
        // Active le Canvas
        transitionCanvas.SetActive(true);
        
        audioSource.PlayOneShot(clip);
        // 🖋️ Écriture progressive du texte
        yield return StartCoroutine(TypewriterEffect(intro));

        // Active la possibilité de quitter (après la fin du texte)
        canExit = true;

        // Attente de la touche E pour quitter
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        transitionCanvas.SetActive(false);
    }

    private IEnumerator TypewriterEffect(string text)
    {
        narrativeText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
