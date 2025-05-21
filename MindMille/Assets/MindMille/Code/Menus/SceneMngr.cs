using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
