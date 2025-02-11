using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class NPCInteract : MonoBehaviour, IInteractable {
    [SerializeField] private string NameNPC;
    [SerializeField] private Transform player;
    private LookAtPlayer lookAtPlayer;

    private Animator animator;
    private FirstPersonController playerMovement;
    
    private int step=0;
    [SerializeField] private GameObject dialogueCanvas;

    [SerializeField] private TMP_Text speakerText;

    [SerializeField] private TMP_Text dialogueText;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        lookAtPlayer = GetComponent<LookAtPlayer>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    public string GetDescription()
    {
        return NameNPC;
    }
    public void Interact(){
        lookAtPlayer.lookat(player.position);

        animator.SetTrigger("Talk");

        if (step == 0){
            showCanvaDialogue("Boubakar","MDRRR je craque mentalement");
            step += 1;
        } else {
            hideCanvaDialogue();
        }
    }

    private void showCanvaDialogue(string nameNPC, string textNPC){

        if (playerMovement == null)
        {
            Debug.LogError("playerMovement est null.");
        }
        if (dialogueCanvas == null)
        {
            Debug.LogError("dialogueCanvas est null.");
        }
        if (speakerText == null)
        {
            Debug.LogError("speakerText est null.");
        }
        if (dialogueText == null)
        {
            Debug.LogError("dialogueText est null.");
        }

        if (playerMovement == null || dialogueCanvas == null || speakerText == null || dialogueText == null)
        {
            return;
        }

        //Time.timeScale = 0f;
        playerMovement.CanMove = false;
        dialogueCanvas.SetActive(true);
        speakerText.text = nameNPC;
        dialogueText.text = textNPC;
    }

    private void hideCanvaDialogue(){
        //Time.timeScale = 1f;
        playerMovement.CanMove = true;
        dialogueCanvas.SetActive(false);
    }
}
