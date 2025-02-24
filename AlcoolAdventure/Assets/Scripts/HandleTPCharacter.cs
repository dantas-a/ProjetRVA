using UnityEngine;

public class HandleTPCharacter : MonoBehaviour
{
    
    public GameObject farmer;
    public GameObject farmer2;
    public GameObject tPose;
    public GameObject demonGirl;
    public LookAtPlayer lookAtPlayerDemon;
    public GameObject hunter;
    public GameObject hunter2;
    public GameObject jug;
    public GameObject guard3;
    public BoxCollider invoc; 
    public NPC jester;


    
    void Start()
    {   
        // Tasse
        jug.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Tavernier 0", () => jug.SetActive(true));

        // T-Pose
        tPose.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => tPose.SetActive(true));

        // Pilleur de tombes
        hunter.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 2", () => hunter.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pilleur 0", () => hunter.SetActive(false));
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => hunter.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pilleur 0", () => EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => hunter2.SetActive(true)));
        ;


        // Démone
        demonGirl.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Collier pris", () => invoc.enabled = true);
        EventSystemManager.Instance.SubscribeToEvent("Demon invoque", () => demonGirl.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Demon invoque", () => invoc.enabled = false);
        EventSystemManager.Instance.SubscribeToEvent("Artefact rendu", () => invoc.enabled = false);
        EventSystemManager.Instance.SubscribeToEvent("Demon disparait", () => demonGirl.SetActive(false));

        // Guarde du cimetière
        guard3.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 2", () => guard3.SetActive(true));

        // Fermier
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Fermier 4", () => farmer.SetActive(false));
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Fermier 4", () => EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => farmer2.SetActive(true)));


        //Bouffon
        EventSystemManager.Instance.SubscribeToEvent("Blague", () => jester.ChangeInteraction(Random.Range(1,12)));
    }

}
