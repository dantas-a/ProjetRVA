using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    //UI references
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

    private bool dialogueActivated;
    private int step = 0;


    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
        //if(Input.GetKeyDown(KeyCode.E) && dialogueActivated == true){
            dialogueCanvas.SetActive(true);
            speakerText.text = speakerName[step];
            dialogueText.text = dialogueContent[step];
            step += 1;
        }
    }

    private void OnTriggerEnter3D(Collider collider3D){
        if(collider3D.gameObject.tag == "Player"){
            dialogueActivated = true;
        }
    }

    private void OnTriggerExit3D(Collider collider){
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
    }
}
