using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseCanvas;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseCanvas.SetActive(true);
        }
    }

    public void ReturnMainMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
