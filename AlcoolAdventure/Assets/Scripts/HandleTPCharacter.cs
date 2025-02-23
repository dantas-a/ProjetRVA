using UnityEngine;

public class HandleTPCharacter : MonoBehaviour
{
    
    public GameObject farmer;
    public GameObject tPose;
    public GameObject demonGirl;
    public GameObject hunter;
    public GameObject jug;
    public GameObject guard3;


    
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
        EventSystemManager.Instance.SubscribeToEvent("Acte 1", () => hunter.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pilleur 0", () => hunter.SetActive(false));
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => {
            hunter.SetActive(true);
            hunter.transform.position = new Vector3(559.2015f, 7.776f, 417.0407f);
        });


        // Démone
        demonGirl.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Demon invoque", () => demonGirl.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Demon disparait", () => demonGirl.SetActive(false));

        // Guarde du cimetière
        guard3.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => guard3.SetActive(true));

        // Fermier
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Fermier 4", () => farmer.SetActive(false));
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => {
            farmer.SetActive(true);
            farmer.transform.position = new Vector3(522.5401f, 0.07999986f, 547.7269f);
        });



    }

}
