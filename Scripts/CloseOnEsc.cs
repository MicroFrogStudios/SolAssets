using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseOnEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GoToIntro()
    {
        SceneManager.LoadScene("cinematica");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene("testing");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
