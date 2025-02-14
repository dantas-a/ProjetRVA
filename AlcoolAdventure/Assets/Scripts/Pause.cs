using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseCanvas;
    private FirstPersonController playerMovement;

    public void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!PauseCanvas.activeSelf) {
                playerMovement.CanMove = false;
                PauseCanvas.SetActive(true);
            } else {
                playerMovement.CanMove = true;
                PauseCanvas.SetActive(false);
            }
        }
    }

    public void ReturnMainMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
