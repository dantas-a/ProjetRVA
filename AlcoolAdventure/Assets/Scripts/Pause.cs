using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseCanvas;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!PauseCanvas.activeSelf) {
                PauseCanvas.SetActive(true);
            } else {
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
