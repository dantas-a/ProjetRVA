using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogueText;

    //UI Content
    [SerializeField]
    private string[] speakerName;

    [SerializeField]
    [TextArea]
    private string[] dialogueContent;

    private int step = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position,interactRange);
            foreach (Collider collider in colliderArray){
                if (collider.TryGetComponent(out NPCInteract npcInteract)){
                    dialogueCanvas.SetActive(true);
                    speakerText.text = speakerName[step];
                    dialogueText.text = dialogueContent[step];
                    step += 1;
                }
            }
        }
        
    }
}
