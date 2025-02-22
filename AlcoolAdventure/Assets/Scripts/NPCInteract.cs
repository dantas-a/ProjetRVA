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

    // private bool hunterFirstTalk = true;

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
        if (!isTyping){
            if (!npc.EndDialogue()) {
                lookAtPlayer.lookat(player.position);

                

                showCanvaDialogue(npc.GetNameNPCTalking(),npc.GetDialogue());
                npc.ShowNextDialogue();
            } else {
                hideCanvaDialogue();
                // if (gameObject.CompareTag("Hunter") && hunterFirstTalk) {
                //     StartCoroutine(moveHunter());
                // }
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
            isTalking = !isTalking;
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
        isTalking = !isTalking;
        if(animator != null) animator.SetBool("isTalking", isTalking);
        playerMovement.CanMove = true;
        dialogueCanvas.SetActive(false);
    }

    // private IEnumerator moveHunter(){
    //     // Pour faire se déplacer le pilleur de tombes la première fois qu'on lui parle
    //     Debug.Log(hunterFirstTalk);
    //     hunterFirstTalk = false;
    //     Vector3 targetPos = new Vector3(479.3787f, 4.16956f, 459.426f);
    //     Quaternion targetRot = new Quaternion();
    //     float rotationSpeed = 5.0f;
    //     float moveDuration = 5.0f;


    //     Vector3 startPos = transform.position;
    //     Quaternion startRot = transform.rotation;
    //     float elapsedTime = 0f;

    //     targetRot = Quaternion.LookRotation(targetPos - startPos);

    //     while (elapsedTime < moveDuration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / moveDuration;

    //         // Interpolation de la position
    //         //transform.position = Vector3.Lerp(startPos, targetPos, t);
    //         transform.position = targetPos;

    //         // Interpolation de la rotation
    //         transform.rotation = Quaternion.Lerp(startRot, targetRot, t * rotationSpeed);

    //         Debug.Log(elapsedTime);
    //         yield return null;
    //     }

    //     // S'assurer que l'objet atteint exactement la cible
    //     transform.position = targetPos;
    //     transform.rotation = targetRot;

        

    // }
}
