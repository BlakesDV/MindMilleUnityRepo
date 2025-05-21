using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTheGame();
        }
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("Quited");
    }

    public void PauseTheGame()
    {
        pauseMenu.SetActive(true);
    }
    public void UnpauseTheGame()
    {
        pauseMenu.SetActive(false);
    }
}
