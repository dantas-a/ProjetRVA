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
        hunter.transform.position = new Vector3(559.2015f, 8.25856f, 417.0407f);
        // Tasse
        jug.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Tavernier 0", () => jug.SetActive(true));

        // T-Pose
        tPose.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => tPose.SetActive(true));

        // Pilleur de tombes
        //hunter.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 1", () => hunter.SetActive(true));
        EventSystemManager.Instance.SubscribeToEvent("Dialogue Pilleur 0", () => hunter.SetActive(false));
        EventSystemManager.Instance.SubscribeToEvent("Acte 2", () => {
            hunter.SetActive(true);
            hunter.transform.position = new Vector3(559.2015f, 7.776f, 417.0407f);
        });


        // Démone
        demonGirl.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Demon invoque", () => demonGirl.SetActive(true));

        // Guarde du cimetière
        guard3.SetActive(false);
        EventSystemManager.Instance.SubscribeToEvent("Acte 2", () => guard3.SetActive(true));

    }

}
