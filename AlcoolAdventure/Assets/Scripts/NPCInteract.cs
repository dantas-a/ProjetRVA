using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using System.Collections;

public class NPCInteract : MonoBehaviour, IInteractable {
    
    public bool canBeInteractedWith { get; set; } = true;
    [SerializeField] private string NameNPC;
    [SerializeField] private Transform player;
    private LookAtPlayer lookAtPlayer;
    [SerializeField] private NPC npc;

    private Animator animator;
    private FirstPersonController playerMovement;
    
    [SerializeField] private GameObject dialogueCanvas;

    [SerializeField] private TMP_Text speakerText;

    [SerializeField] private TMP_Text dialogueText;

    private bool isTyping;

    private bool isTalking = false;

    private GameObject pegu;

    public void Start()
    {
        animator = GetComponent<Animator>();
        lookAtPlayer = GetComponent<LookAtPlayer>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Fermier 5", () => fight());
        pegu = GameObject.FindGameObjectWithTag("BagarrePegu");
    }

    public string GetDescription()
    {
        return NameNPC;
    }
    public void Interact(){
        if (!isTyping){
            if (!npc.EndDialogue()) {
                lookAtPlayer.lookat(player.position);

                

                showCanvaDialogue(npc.GetNameNPCTalking(),npc.GetDialogue());
                npc.ShowNextDialogue();
            } else {
                hideCanvaDialogue();
            }
        }
    }

    IEnumerator Typing(string textNPC)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in textNPC.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }
    private void showCanvaDialogue(string nameNPC, string textNPC){
        if (nameNPC != null && textNPC != null) {   
            //Time.timeScale = 0f;
            isTalking = true;
            if(animator != null) animator.SetBool("isTalking", isTalking);
            playerMovement.CanMove = false;
            dialogueCanvas.SetActive(true);
            speakerText.text = nameNPC;
            npc.PlayDialogueAudio();
            StartCoroutine(Typing(textNPC));
        }
    }

    private void hideCanvaDialogue(){
        //Time.timeScale = 1f;
        isTalking = false;
        if(animator != null) animator.SetBool("isTalking", isTalking);
        playerMovement.CanMove = true;
        dialogueCanvas.SetActive(false);
    }

    private void fight(){
        if(CompareTag("BagarreFermier")){
            animator.SetTrigger("fightFarmer");
            canBeInteractedWith = false;
            pegu.GetComponent<LookAtPlayer>().lookat(transform.position);
            lookAtPlayer.lookat(pegu.GetComponent<Transform>().position);
            pegu.GetComponent<Animator>().SetTrigger("fightBandit");
            pegu.GetComponent<NPCInteract>().canBeInteractedWith = false;
            Invoke("delayFarmerFight", 15.0f);
        }
    }

    private void delayFarmerFight(){
        canBeInteractedWith = true;
    }
}
