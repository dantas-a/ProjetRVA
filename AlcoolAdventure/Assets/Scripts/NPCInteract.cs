using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class NPCInteract : MonoBehaviour, IInteractable {
    
    public bool canBeInteractedWith { get; set; } = true;
    [SerializeField] private string NameNPC;
    [SerializeField] private Transform player;
    private LookAtPlayer lookAtPlayer;
    [SerializeField] private NPC npc;

    //private Animator animator;
    private FirstPersonController playerMovement;
    
    [SerializeField] private GameObject dialogueCanvas;

    [SerializeField] private TMP_Text speakerText;

    [SerializeField] private TMP_Text dialogueText;

    private bool isTalking;

    public void Awake()
    {
        //animator = GetComponent<Animator>();
        lookAtPlayer = GetComponent<LookAtPlayer>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    public string GetDescription()
    {
        return NameNPC;
    }
    public void Interact(){
        if (!npc.EndDialogue()) {
            lookAtPlayer.lookat(player.position);

            //animator.SetTrigger("Talk");

            showCanvaDialogue(npc.GetNameNPCTalking(),npc.GetDialogue());
            npc.ShowNextDialogue();
        } else {
            hideCanvaDialogue();
        }
    }

    private void showCanvaDialogue(string nameNPC, string textNPC){
        if (nameNPC != null && textNPC != null) {   
            //Time.timeScale = 0f;
            playerMovement.CanMove = false;
            dialogueCanvas.SetActive(true);
            speakerText.text = nameNPC;
            dialogueText.text = textNPC;
            npc.PlayDialogueAudio();
        }
    }

    private void hideCanvaDialogue(){
        //Time.timeScale = 1f;
        playerMovement.CanMove = true;
        dialogueCanvas.SetActive(false);
    }
}
